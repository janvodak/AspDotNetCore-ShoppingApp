using System.Reflection;

namespace ShoppingApp.Services.Order.API.Domain.Shared
{
	public abstract class EnumerationBase : IComparable
	{
		protected EnumerationBase(int id, string name) => (Id, Name) = (id, name);

		public int Id { get; private set; }
		public string Name { get; private set; }

		public override string ToString() => Name;

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

		public int CompareTo(object other) => Id.CompareTo(((EnumerationBase)other).Id);
	}
}
