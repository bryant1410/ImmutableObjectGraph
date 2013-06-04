﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ImmutableTree Version: 0.0.0.1
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

namespace Demo {
	using System.Diagnostics;
	using System.Linq;
	using ImmutableObjectGraph;
	
	public interface IFruit {
		System.String Color { get; }
		System.Int32 SkinThickness { get; }
	}
	
	public partial class Fruit : IFruit {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static readonly Fruit DefaultInstance = GetDefaultTemplate();
		
		/// <summary>The last identity assigned to a created instance.</summary>
		private static int lastIdentityProduced;
	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly System.String color;
	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly System.Int32 skinThickness;
	
		private readonly System.Int32 identity;
	
		/// <summary>Initializes a new instance of the Fruit class.</summary>
		protected Fruit(
			System.Int32 identity,
			System.String color,
			System.Int32 skinThickness)
		{
			this.identity = identity;
			this.color = color;
			this.skinThickness = skinThickness;
			this.Validate();
		}
	
		public static Fruit Create(
			ImmutableObjectGraph.Optional<System.String> color = default(ImmutableObjectGraph.Optional<System.String>),
			ImmutableObjectGraph.Optional<System.Int32> skinThickness = default(ImmutableObjectGraph.Optional<System.Int32>)) {
			var identity = Optional.For(NewIdentity());
			return DefaultInstance.WithFactory(
				color: Optional.For(color.GetValueOrDefault(DefaultInstance.Color)),
				skinThickness: Optional.For(skinThickness.GetValueOrDefault(DefaultInstance.SkinThickness)),
				identity: Optional.For(identity.GetValueOrDefault(DefaultInstance.Identity)));
		}
	
		internal static Fruit CreateWithIdentity(
			ImmutableObjectGraph.Optional<System.String> color = default(ImmutableObjectGraph.Optional<System.String>),
			ImmutableObjectGraph.Optional<System.Int32> skinThickness = default(ImmutableObjectGraph.Optional<System.Int32>),
			ImmutableObjectGraph.Optional<System.Int32> identity = default(ImmutableObjectGraph.Optional<System.Int32>)) {
			if (!identity.IsDefined) {
				identity = NewIdentity();
			}
	
			return DefaultInstance.WithFactory(
				color: Optional.For(color.GetValueOrDefault(DefaultInstance.Color)),
				skinThickness: Optional.For(skinThickness.GetValueOrDefault(DefaultInstance.SkinThickness)),
				identity: Optional.For(identity.GetValueOrDefault(DefaultInstance.Identity)));
		}
	
		public System.String Color {
			get { return this.color; }
		}
	
		public System.Int32 SkinThickness {
			get { return this.skinThickness; }
		}
		
		/// <summary>Returns a new instance with the Color property set to the specified value.</summary>
		public Fruit WithColor(System.String value) {
			if (value == this.Color) {
				return this;
			}
		
			return this.With(color: Optional.For(value));
		}
		
		/// <summary>Returns a new instance with the SkinThickness property set to the specified value.</summary>
		public Fruit WithSkinThickness(System.Int32 value) {
			if (value == this.SkinThickness) {
				return this;
			}
		
			return this.With(skinThickness: Optional.For(value));
		}
		
		/// <summary>Returns a new instance of this object with any number of properties changed.</summary>
		public virtual Fruit With(
			ImmutableObjectGraph.Optional<System.String> color = default(ImmutableObjectGraph.Optional<System.String>),
			ImmutableObjectGraph.Optional<System.Int32> skinThickness = default(ImmutableObjectGraph.Optional<System.Int32>)) {
			var identity = default(ImmutableObjectGraph.Optional<System.Int32>);
			return this.WithFactory(
				color: Optional.For(color.GetValueOrDefault(this.Color)),
				skinThickness: Optional.For(skinThickness.GetValueOrDefault(this.SkinThickness)),
				identity: Optional.For(identity.GetValueOrDefault(this.Identity)));
		}
	
		/// <summary>Returns a new instance of this object with any number of properties changed.</summary>
		private Fruit WithFactory(
			ImmutableObjectGraph.Optional<System.String> color = default(ImmutableObjectGraph.Optional<System.String>),
			ImmutableObjectGraph.Optional<System.Int32> skinThickness = default(ImmutableObjectGraph.Optional<System.Int32>),
			ImmutableObjectGraph.Optional<System.Int32> identity = default(ImmutableObjectGraph.Optional<System.Int32>)) {
			if (
				(identity.IsDefined && identity.Value != this.Identity) || 
				(color.IsDefined && color.Value != this.Color) || 
				(skinThickness.IsDefined && skinThickness.Value != this.SkinThickness)) {
				return new Fruit(
					identity: identity.GetValueOrDefault(this.Identity),
					color: color.GetValueOrDefault(this.Color),
					skinThickness: skinThickness.GetValueOrDefault(this.SkinThickness));
			} else {
				return this;
			}
		}
	
		protected internal System.Int32 Identity {
			get { return this.identity; }
		}
	
		/// <summary>Returns a unique identity that may be assigned to a newly created instance.</summary>
		protected static System.Int32 NewIdentity() {
			return System.Threading.Interlocked.Increment(ref lastIdentityProduced);
		}
	
		/// <summary>Normalizes and/or validates all properties on this object.</summary>
		/// <exception type="ArgumentException">Thrown if any properties have disallowed values.</exception>
		partial void Validate();
	
		/// <summary>Provides defaults for fields.</summary>
		/// <param name="template">The struct to set default values on.</param>
		static partial void CreateDefaultTemplate(ref Template template);
	
		/// <summary>Returns a newly instantiated Fruit whose fields are initialized with default values.</summary>
		private static Fruit GetDefaultTemplate() {
			var template = new Template();
			CreateDefaultTemplate(ref template);
			return new Fruit(
				default(System.Int32), 
				template.Color, 
				template.SkinThickness);
		}
	
		/// <summary>A struct with all the same fields as the containing type for use in describing default values for new instances of the class.</summary>
		private struct Template {
			internal System.String Color { get; set; }
	
			internal System.Int32 SkinThickness { get; set; }
		}
		
		public Builder ToBuilder() {
			return new Builder(this);
		}
		
		public partial class Builder {
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private Fruit immutable;
		
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			protected System.String color;
		
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			protected System.Int32 skinThickness;
		
			internal Builder(Fruit immutable) {
				this.immutable = immutable;
		
				this.color = immutable.Color;
				this.skinThickness = immutable.SkinThickness;
			}
		
			public System.String Color {
				get {
					return this.color;
				}
		
				set {
					this.color = value;
				}
			}
		
			public System.Int32 SkinThickness {
				get {
					return this.skinThickness;
				}
		
				set {
					this.skinThickness = value;
				}
			}
		
			public Fruit ToImmutable() {
				return this.immutable = this.immutable.With(
					ImmutableObjectGraph.Optional.For(this.Color),
					ImmutableObjectGraph.Optional.For(this.SkinThickness));
			}
		}
	}
}


