﻿<#
this.HandleType += (templateType, position) => {
	switch (position) {
		case HandleTypePosition.InsideConstructor:
			if (templateType.IsRecursive) {
#>
		this.InitializeLookup(<#= templateType.LookupTableField.NameCamelCase #>);
<#
			}
			
			break;

		case HandleTypePosition.WithinTypeDeclaration:
			if (templateType.IsRecursiveType) {
				var recursiveType = templateType.RecursiveTypeFromFamily;
			}

			if (templateType.IsRecursive) {
#>

private static readonly System.Collections.Immutable.ImmutableDictionary<<#= templateType.RequiredIdentityField.TypeName #>, System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>>> lookupTableLazySentinal = System.Collections.Immutable.ImmutableDictionary.Create<<#= templateType.RequiredIdentityField.TypeName #>, System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>>>().Add(default(<#= templateType.RequiredIdentityField.TypeName #>), new System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>>());

private <#= templateType.NodeLookupTableTypeName #> lookupTable;

private int inefficiencyLoad;

/// <summary>
/// The maximum number of steps allowable for a search to be done among this node's children
/// before a faster lookup table will be built.
/// </summary>
internal const int InefficiencyLoadThreshold = 16;

private <#= templateType.NodeLookupTableTypeName #> LookupTable {
	get {
		if (this.lookupTable == lookupTableLazySentinal) {
			this.lookupTable = this.CreateLookupTable();
			this.inefficiencyLoad = 1;
		}

		return this.lookupTable;
	}
}

private void InitializeLookup(ImmutableObjectGraph.Optional<<#= templateType.NodeLookupTableTypeName #>> priorLookupTable = default(ImmutableObjectGraph.Optional<<#= templateType.NodeLookupTableTypeName #>>)) {
	this.inefficiencyLoad = 1;
	if (priorLookupTable.IsDefined && priorLookupTable.Value != null) {
		this.lookupTable = priorLookupTable.Value;
	} else {
		if (this.<#= templateType.RecursiveField.NameCamelCase #> != null) {
			foreach (var child in this.<#= templateType.RecursiveField.NameCamelCase #>)
			{
				var recursiveChild = child as <#= templateType.RecursiveParent.TypeName #>;
				this.inefficiencyLoad += recursiveChild != null ? recursiveChild.inefficiencyLoad : 1;
			}
		}

		if (this.inefficiencyLoad > InefficiencyLoadThreshold) {
			this.inefficiencyLoad = 1;
			this.lookupTable = lookupTableLazySentinal;
		}
	}

	this.ValidateInternalIntegrityDebugOnly();
}

/// <summary>
/// Creates the lookup table that will contain all this node's children.
/// </summary>
/// <returns>The lookup table.</returns>
private <#= templateType.NodeLookupTableTypeName #> CreateLookupTable() {
	var table = System.Collections.Immutable.ImmutableDictionary.Create<<#= templateType.RequiredIdentityField.TypeName #>, System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>>>().ToBuilder();
	this.ContributeDescendentsToLookupTable(table);
	return table.ToImmutable();
}

/// <summary>
/// Adds this node's children (recursively) to the lookup table.
/// </summary>
/// <param name="seedLookupTable">The lookup table to add entries to.</param>
/// <returns>The new lookup table.</returns>
private void ContributeDescendentsToLookupTable(<#= templateType.NodeLookupTableTypeName #>.Builder seedLookupTable)
{
	foreach (var child in this.<#= templateType.RecursiveField.NamePascalCase #>)
	{
		seedLookupTable.Add(child.Identity, new System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>>(child, this.Identity));
		var recursiveChild = child as <#= templateType.RecursiveParent.TypeName #>;
		if (recursiveChild != null) {
			recursiveChild.ContributeDescendentsToLookupTable(seedLookupTable);
		}
	}
}

public bool TryFind(<#= templateType.RequiredIdentityField.TypeName #> identity, out <#= templateType.RecursiveType.TypeName #> value) {
	if (this.Identity.Equals(identity)) {
		value = this;
		return true;
	}

	if (this.LookupTable != null) {
		System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>> lookupValue;
		if (this.LookupTable.TryGetValue(identity, out lookupValue)) {
			value = lookupValue.Key;
			return true;
		}
	} else {
		// No lookup table means we have to exhaustively search each child and its descendents.
		foreach (var child in this.Children) {
			var recursiveChild = child as <#= templateType.RecursiveParent.TypeName #>;
			if (recursiveChild != null) {
				if (recursiveChild.TryFind(identity, out value)) {
					return true;
				}
			} else {
				if (child.Identity.Equals(identity)) {
					value = child;
					return true;
				}
			}
		}
	}

	value = null;
	return false;
}

public <#= templateType.RecursiveType.TypeName #> Find(<#= templateType.RequiredIdentityField.TypeName #> identity) {
	<#= templateType.RecursiveType.TypeName #> result;
	if (this.TryFind(identity, out result)) {
		return result;
	}

	throw new System.Collections.Generic.KeyNotFoundException();
}

public bool TryFindImmediateChild(<#= templateType.RequiredIdentityField.TypeName #> identity, out <#= templateType.RecursiveType.TypeName #> value) {
	if (this.LookupTable != null) {
		System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>> lookupValue;
		if (this.LookupTable.TryGetValue(identity, out lookupValue) && lookupValue.Value == this.<#= templateType.RequiredIdentityField.NamePascalCase #>) {
			value = lookupValue.Key;
			return true;
		}
	} else {
		// No lookup table means we have to exhaustively search each child.
		foreach (var child in this.Children) {
			if (child.Identity.Equals(identity)) {
				value = child;
				return true;
			}
		}
	}

	value = null;
	return false;
}

/// <summary>Checks whether an object with the specified identity is among this object's descendents.</summary>
public bool Contains(<#= templateType.RequiredIdentityField.TypeName #> identity) {
	<#= templateType.RecursiveType.TypeName #> result;
	return this.TryFind(identity, out result) && result != this;
}

/// <summary>Gets the recursive parent of the specified value, or <c>null</c> if none could be found.</summary>
internal ParentedRecursiveType<<#= templateType.RecursiveParent.TypeName #>, <#= templateType.RecursiveTypeFromFamily.TypeName #>> GetParentedNode(<#= templateType.RequiredIdentityField.TypeName #> identity) {
	if (this.Identity == identity) {
		return new ParentedRecursiveType<<#= templateType.RecursiveParent.TypeName #>, <#= templateType.RecursiveTypeFromFamily.TypeName #>>(this, null);
	}

	if (this.LookupTable != null) {
		System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>> lookupValue;
		if (this.LookupTable.TryGetValue(identity, out lookupValue)) {
			var parentIdentity = lookupValue.Value;
			return new ParentedRecursiveType<<#= templateType.RecursiveParent.TypeName #>, <#= templateType.RecursiveTypeFromFamily.TypeName #>>(this.LookupTable[identity].Key, (<#= templateType.RecursiveParent.TypeName #>)this.Find(parentIdentity));
		}
	} else {
		// No lookup table means we have to aggressively search each child.
		foreach (var child in this.Children) {
			if (child.Identity.Equals(identity)) {
				return new ParentedRecursiveType<<#= templateType.RecursiveParent.TypeName #>, <#= templateType.RecursiveTypeFromFamily.TypeName #>>(child, this);
			}

			var recursiveChild = child as <#= templateType.RecursiveParent.TypeName #>;
			if (recursiveChild != null) {
				var childResult = recursiveChild.GetParentedNode(identity);
				if (childResult.Value != null) {
					return childResult;
				}
			} 
		}
	}

	return default(ParentedRecursiveType<<#= templateType.RecursiveParent.TypeName #>, <#= templateType.RecursiveTypeFromFamily.TypeName #>>);
}

/// <summary>Gets the recursive parent of the specified value, or <c>null</c> if none could be found.</summary>
internal <#= templateType.RecursiveParent.TypeName #> GetParent(<#= templateType.RecursiveType.TypeName #> descendent) {
	return this.GetParentedNode(descendent.<#= templateType.RequiredIdentityField.NamePascalCase #>).Parent;
}

public System.Collections.Immutable.ImmutableStack<<#= templateType.RecursiveType.TypeName #>> GetSpine(<#= templateType.RequiredIdentityField.TypeName #> descendent) {
	var emptySpine = System.Collections.Immutable.ImmutableStack.Create<<#= templateType.RecursiveType.TypeName #>>();
	if (this.Identity.Equals(descendent)) {
		return emptySpine.Push(this);
	}

	if (this.LookupTable != null) {
		System.Collections.Generic.KeyValuePair<<#= templateType.RecursiveType.TypeName #>, <#= templateType.RequiredIdentityField.TypeName #>> lookupValue;
		if (this.LookupTable.TryGetValue(descendent, out lookupValue))
		{
			// Awesome.  We know the node the caller is looking for is a descendent of this node.
			// Now just string together all the nodes that connect this one with the sought one.
			var spine = emptySpine;
			do
			{
				spine = spine.Push(lookupValue.Key);
			}
			while (this.lookupTable.TryGetValue(lookupValue.Value, out lookupValue));
			return spine.Push(this);
		}
	} else {
		// We don't have an efficient lookup table for this node.  Aggressively search every child.
		var spine = emptySpine;
		foreach (var child in this.Children) {
			var recursiveChild = child as <#= templateType.RecursiveParent.TypeName #>;
			if (recursiveChild != null) {
				spine = recursiveChild.GetSpine(descendent);
			} else if (child.Identity.Equals(descendent)) {
				spine = spine.Push(child);
			}

			if (!spine.IsEmpty) {
				return spine.Push(this);
			}
		}
	}

	// The descendent is not in this sub-tree.
	return emptySpine;
}

public System.Collections.Immutable.ImmutableStack<<#= templateType.RecursiveType.TypeName #>> GetSpine(<#= templateType.RecursiveType.TypeName #> descendent) {
	return this.GetSpine(descendent.Identity);
}
<#
			}

			break;
		default:
			break;
	}
}; // event handler
#>