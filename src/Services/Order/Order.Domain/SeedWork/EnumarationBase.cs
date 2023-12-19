using System.Reflection;

namespace ShoppingApp.Services.Order.API.Domain.SeedWork
{
	public abstract class EnumerationBase : IComparable
	{
		protected EnumerationBase(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public int Id { get; private set; }

		public string Name { get; private set; }

		public static IEnumerable<T> GetAll<T>() where T : EnumerationBase
		{
			return typeof(T)
				.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
				.Select(field => field.GetValue(null))
				.OfType<T>();
		}

		public override bool Equals(object? obj)
		{
			if (obj is not EnumerationBase otherValue)
			{
				return false;
			}

			bool typeMatches = GetType().Equals(obj.GetType());
			bool valueMatches = Id.Equals(otherValue.Id);

			return typeMatches && valueMatches;
		}

		public int CompareTo(object? other)
		{
			if (other == null || other is not EnumerationBase)
			{
				return 1;
			}

			return Id.CompareTo(((EnumerationBase)other).Id);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
