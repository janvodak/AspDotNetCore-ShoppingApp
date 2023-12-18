namespace ShoppingApp.Services.Order.API.Domain.Shared
{
	public abstract class ValueObjectBase
	{
		protected static bool EqualOperator(ValueObjectBase left, ValueObjectBase right)
		{
			if (left is null ^ right is null)
			{
				return false;
			}

			return ReferenceEquals(left, right) || left.Equals(right);
		}

		protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right)
		{
			return !EqualOperator(left, right);
		}

		protected abstract IEnumerable<object> GetEqualityComponents();

		public override bool Equals(object? obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}

			var other = (ValueObjectBase)obj;

			return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		public override int GetHashCode()
		{
			return GetEqualityComponents()
				.Select(x => x != null ? x.GetHashCode() : 0)
				.Aggregate((x, y) => x ^ y);
		}

		public static bool operator ==(ValueObjectBase one, ValueObjectBase two)
		{
			return EqualOperator(one, two);
		}

		public static bool operator !=(ValueObjectBase one, ValueObjectBase two)
		{
			return NotEqualOperator(one, two);
		}
	}
}
