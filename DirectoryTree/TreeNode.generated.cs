﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ImmutableTree Version: 0.0.0.1
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

namespace DirectoryTree {
	using System.Diagnostics;
	using ImmutableObjectGraph;

	
	public interface ITreeNode {
		System.String Caption { get; }
		System.String FilePath { get; }
		System.Boolean Visible { get; }
		System.Collections.Immutable.ImmutableHashSet<System.String> Attributes { get; }
		System.Collections.Immutable.ImmutableList<TreeNode> Children { get; }
	}
	
	public partial class TreeNode : System.Object, ITreeNode, System.Collections.Generic.IEnumerable<TreeNode> {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static readonly TreeNode DefaultInstance = GetDefaultTemplate();
	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly System.String caption;
	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly System.String filePath;
	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly System.Boolean visible;
	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly System.Collections.Immutable.ImmutableHashSet<System.String> attributes;
	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly System.Collections.Immutable.ImmutableList<TreeNode> children;
	
		/// <summary>Initializes a new instance of the TreeNode class.</summary>
		protected TreeNode()
		{
		}
	
		/// <summary>Initializes a new instance of the TreeNode class.</summary>
		protected TreeNode(System.String caption, System.String filePath, System.Boolean visible, System.Collections.Immutable.ImmutableHashSet<System.String> attributes, System.Collections.Immutable.ImmutableList<TreeNode> children)
			: base()
		{
			this.caption = caption;
			this.filePath = filePath;
			this.visible = visible;
			this.attributes = attributes;
			this.children = children;
			this.Validate();
		}
	
		public static TreeNode Create(
			ImmutableObjectGraph.Optional<System.String> caption = default(ImmutableObjectGraph.Optional<System.String>), 
			ImmutableObjectGraph.Optional<System.String> filePath = default(ImmutableObjectGraph.Optional<System.String>), 
			ImmutableObjectGraph.Optional<System.Boolean> visible = default(ImmutableObjectGraph.Optional<System.Boolean>), 
			ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableHashSet<System.String>> attributes = default(ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableHashSet<System.String>>), 
			ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableList<TreeNode>> children = default(ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableList<TreeNode>>)) {
			return DefaultInstance.With(
				caption.IsDefined ? caption : ImmutableObjectGraph.Optional.For(DefaultInstance.Caption), 
				filePath.IsDefined ? filePath : ImmutableObjectGraph.Optional.For(DefaultInstance.FilePath), 
				visible.IsDefined ? visible : ImmutableObjectGraph.Optional.For(DefaultInstance.Visible), 
				attributes.IsDefined ? attributes : ImmutableObjectGraph.Optional.For(DefaultInstance.Attributes), 
				children.IsDefined ? children : ImmutableObjectGraph.Optional.For(DefaultInstance.Children));
		}
	
		public System.String Caption {
			get { return this.caption; }
		}
	
		public System.String FilePath {
			get { return this.filePath; }
		}
	
		public System.Boolean Visible {
			get { return this.visible; }
		}
	
		public System.Collections.Immutable.ImmutableHashSet<System.String> Attributes {
			get { return this.attributes; }
		}
	
		public System.Collections.Immutable.ImmutableList<TreeNode> Children {
			get { return this.children; }
		}
		/// <summary>Returns a new instance with the Caption property set to the specified value.</summary>
		public TreeNode WithCaption(System.String value) {
			if (value == this.Caption) {
				return this;
			}
	
			return this.With(caption: value);
		}
		/// <summary>Returns a new instance with the FilePath property set to the specified value.</summary>
		public TreeNode WithFilePath(System.String value) {
			if (value == this.FilePath) {
				return this;
			}
	
			return this.With(filePath: value);
		}
		/// <summary>Returns a new instance with the Visible property set to the specified value.</summary>
		public TreeNode WithVisible(System.Boolean value) {
			if (value == this.Visible) {
				return this;
			}
	
			return this.With(visible: value);
		}
		/// <summary>Returns a new instance with the Attributes property set to the specified value.</summary>
		public TreeNode WithAttributes(System.Collections.Immutable.ImmutableHashSet<System.String> value) {
			if (value == this.Attributes) {
				return this;
			}
	
			return this.With(attributes: value);
		}
	
		/// <summary>Replaces the elements of the Attributes collection with the specified collection.</summary>
		public TreeNode WithAttributes(params System.String[] values) {
			return this.With(attributes: this.Attributes.ResetContents(values));
		}
	
		/// <summary>Replaces the elements of the Attributes collection with the specified collection.</summary>
		public TreeNode WithAttributes(System.Collections.Generic.IEnumerable<System.String> values) {
			return this.With(attributes: this.Attributes.ResetContents(values));
		}
	
		/// <summary>Adds the specified elements from the Attributes collection.</summary>
		public TreeNode AddAttributes(System.Collections.Generic.IEnumerable<System.String> values) {
			return this.With(attributes: this.Attributes.AddRange(values));
		}
	
		/// <summary>Adds the specified elements from the Attributes collection.</summary>
		public TreeNode AddAttributes(params System.String[] values) {
			return this.With(attributes: this.Attributes.AddRange(values));
		}
	
		/// <summary>Adds the specified element from the Attributes collection.</summary>
		public TreeNode AddAttributes(System.String value) {
			return this.With(attributes: this.Attributes.Add(value));
		}
	
		/// <summary>Removes the specified elements from the Attributes collection.</summary>
		public TreeNode RemoveAttributes(System.Collections.Generic.IEnumerable<System.String> values) {
			return this.With(attributes: this.Attributes.RemoveRange(values));
		}
	
		/// <summary>Removes the specified elements from the Attributes collection.</summary>
		public TreeNode RemoveAttributes(params System.String[] values) {
			return this.With(attributes: this.Attributes.RemoveRange(values));
		}
	
		/// <summary>Removes the specified element from the Attributes collection.</summary>
		public TreeNode RemoveAttributes(System.String value) {
			return this.With(attributes: this.Attributes.Remove(value));
		}
	
		/// <summary>Clears all elements from the Attributes collection.</summary>
		public TreeNode RemoveAttributes() {
			return this.With(attributes: this.Attributes.Clear());
		}
		/// <summary>Returns a new instance with the Children property set to the specified value.</summary>
		public TreeNode WithChildren(System.Collections.Immutable.ImmutableList<TreeNode> value) {
			if (value == this.Children) {
				return this;
			}
	
			return this.With(children: value);
		}
	
		/// <summary>Replaces the elements of the Children collection with the specified collection.</summary>
		public TreeNode WithChildren(params TreeNode[] values) {
			return this.With(children: this.Children.ResetContents(values));
		}
	
		/// <summary>Replaces the elements of the Children collection with the specified collection.</summary>
		public TreeNode WithChildren(System.Collections.Generic.IEnumerable<TreeNode> values) {
			return this.With(children: this.Children.ResetContents(values));
		}
	
		/// <summary>Adds the specified elements from the Children collection.</summary>
		public TreeNode AddChildren(System.Collections.Generic.IEnumerable<TreeNode> values) {
			return this.With(children: this.Children.AddRange(values));
		}
	
		/// <summary>Adds the specified elements from the Children collection.</summary>
		public TreeNode AddChildren(params TreeNode[] values) {
			return this.With(children: this.Children.AddRange(values));
		}
	
		/// <summary>Adds the specified element from the Children collection.</summary>
		public TreeNode AddChildren(TreeNode value) {
			return this.With(children: this.Children.Add(value));
		}
	
		/// <summary>Removes the specified elements from the Children collection.</summary>
		public TreeNode RemoveChildren(System.Collections.Generic.IEnumerable<TreeNode> values) {
			return this.With(children: this.Children.RemoveRange(values));
		}
	
		/// <summary>Removes the specified elements from the Children collection.</summary>
		public TreeNode RemoveChildren(params TreeNode[] values) {
			return this.With(children: this.Children.RemoveRange(values));
		}
	
		/// <summary>Removes the specified element from the Children collection.</summary>
		public TreeNode RemoveChildren(TreeNode value) {
			return this.With(children: this.Children.Remove(value));
		}
	
		/// <summary>Clears all elements from the Children collection.</summary>
		public TreeNode RemoveChildren() {
			return this.With(children: this.Children.Clear());
		}
	
		/// <summary>Returns a new instance of this object with any number of properties changed.</summary>
		public virtual TreeNode With(
			ImmutableObjectGraph.Optional<System.String> caption = default(ImmutableObjectGraph.Optional<System.String>), 
			ImmutableObjectGraph.Optional<System.String> filePath = default(ImmutableObjectGraph.Optional<System.String>), 
			ImmutableObjectGraph.Optional<System.Boolean> visible = default(ImmutableObjectGraph.Optional<System.Boolean>), 
			ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableHashSet<System.String>> attributes = default(ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableHashSet<System.String>>), 
			ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableList<TreeNode>> children = default(ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableList<TreeNode>>)) {
			if (
				(caption.IsDefined && caption.Value != this.Caption) || 
				(filePath.IsDefined && filePath.Value != this.FilePath) || 
				(visible.IsDefined && visible.Value != this.Visible) || 
				(attributes.IsDefined && attributes.Value != this.Attributes) || 
				(children.IsDefined && children.Value != this.Children)) {
				return new TreeNode(
					caption.IsDefined ? caption.Value : this.Caption,
					filePath.IsDefined ? filePath.Value : this.FilePath,
					visible.IsDefined ? visible.Value : this.Visible,
					attributes.IsDefined ? attributes.Value : this.Attributes,
					children.IsDefined ? children.Value : this.Children);
			} else {
				return this;
			}
		}
	
	
		public Builder ToBuilder() {
			return new Builder(this);
		}
	
	 	public System.Collections.Generic.IEnumerator<TreeNode> GetEnumerator() {
			return this.children.GetEnumerator();
		}
	
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.children.GetEnumerator();
		}
	
	 
		/// <summary>Normalizes and/or validates all properties on this object.</summary>
		/// <exception type="ArgumentException">Thrown if any properties have disallowed values.</exception>
		partial void Validate();
	
		/// <summary>Provides defaults for fields.</summary>
		/// <param name="template">The struct to set default values on.</param>
		static partial void CreateDefaultTemplate(ref Template template);
	
		/// <summary>Returns a newly instantiated TreeNode whose fields are initialized with default values.</summary>
		private static TreeNode GetDefaultTemplate() {
			var template = new Template();
			CreateDefaultTemplate(ref template);
			return new TreeNode(
				template.Caption, 
				template.FilePath, 
				template.Visible, 
				template.Attributes, 
				template.Children);
		}
	
		public partial class Builder {
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private TreeNode immutable;
	
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private System.String caption;
	
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private System.String filePath;
	
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private System.Boolean visible;
	
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableHashSet<System.String>.Builder> attributes;
	
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private ImmutableObjectGraph.Optional<System.Collections.Immutable.ImmutableList<TreeNode>.Builder> children;
	
			internal Builder(TreeNode immutable) {
				this.immutable = immutable;
	
				this.caption = immutable.Caption;
				this.filePath = immutable.FilePath;
				this.visible = immutable.Visible;
			}
	
			public System.String Caption {
				get {
					return this.caption;
				}
	
				set {
					this.caption = value;
				}
			}
	
			public System.String FilePath {
				get {
					return this.filePath;
				}
	
				set {
					this.filePath = value;
				}
			}
	
			public System.Boolean Visible {
				get {
					return this.visible;
				}
	
				set {
					this.visible = value;
				}
			}
	
			public System.Collections.Immutable.ImmutableHashSet<System.String>.Builder Attributes {
				get {
					if (!this.attributes.IsDefined) {
						this.attributes = this.immutable.attributes != null ? this.immutable.attributes.ToBuilder() : null;
					}
	
					return this.attributes.Value;
				}
	
				set {
					this.attributes = value;
				}
			}
	
			public System.Collections.Immutable.ImmutableList<TreeNode>.Builder Children {
				get {
					if (!this.children.IsDefined) {
						this.children = this.immutable.children != null ? this.immutable.children.ToBuilder() : null;
					}
	
					return this.children.Value;
				}
	
				set {
					this.children = value;
				}
			}
	
			public TreeNode ToImmutable() {
				var attributes = this.attributes.IsDefined ? (this.attributes.Value != null ? this.attributes.Value.ToImmutable() : null) : this.immutable.attributes;
				var children = this.children.IsDefined ? (this.children.Value != null ? this.children.Value.ToImmutable() : null) : this.immutable.children;
				return this.immutable = this.immutable.With(
					ImmutableObjectGraph.Optional.For(this.Caption),
					ImmutableObjectGraph.Optional.For(this.FilePath),
					ImmutableObjectGraph.Optional.For(this.Visible),
					ImmutableObjectGraph.Optional.For(attributes),
					ImmutableObjectGraph.Optional.For(children));
			}
		}
	
		/// <summary>A struct with all the same fields as the containing type for use in describing default values for new instances of the class.</summary>
		private struct Template {
			internal System.String Caption { get; set; }
	
			internal System.String FilePath { get; set; }
	
			internal System.Boolean Visible { get; set; }
	
			internal System.Collections.Immutable.ImmutableHashSet<System.String> Attributes { get; set; }
	
			internal System.Collections.Immutable.ImmutableList<TreeNode> Children { get; set; }
		}
	}
}

