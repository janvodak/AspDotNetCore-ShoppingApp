using System;
using System.Reflection;

namespace Order.Domain.Src.Shared
{
	public abstract class EnumerationBase : IComparable
	{
		public int Id { get; private set; }
		public string Name { get; private set; }

		protected EnumerationBase(int id, string name) => (this.Id, this.Name) = (id, name);

		public override string ToString() => this.Name;

		public static IEnumerable<T> GetAll<T>() where T : EnumerationBase =>
			typeof(T).GetFields(BindingFlags.Public |
								BindingFlags.Static |
								BindingFlags.DeclaredOnly)
					 .Select(f => f.GetValue(null))
					 .Cast<T>();

		public override bool Equals(object obj)
		{
			if (obj is not EnumerationBase otherValue)
			{
				return false;
			}

			var typeMatches = GetType().Equals(obj.GetType());
			var valueMatches = Id.Equals(otherValue.Id);

			return typeMatches && valueMatches;
		}

		public int CompareTo(object other) => this.Id.CompareTo(((EnumerationBase)other).Id);
	}
}
