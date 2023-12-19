using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Shared
{
	public class QuantityValueObject : ValueObjectBase
	{
		public QuantityValueObject() { }

		public QuantityValueObject(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(
					nameof(value),
					"Quantity must be greater than or equal to 0.");
			}

			Value = value;
		}

		public int Value { get; private set; }

		public QuantityValueObject Increase(QuantityValueObject increment)
		{
			return new QuantityValueObject(Value + increment.Value);
		}

		public QuantityValueObject Decrease(QuantityValueObject decrement)
		{
			return new QuantityValueObject(Value - decrement.Value);
		}

		public QuantityValueObject Multiply(QuantityValueObject multiplier)
		{
			return new QuantityValueObject(Value * multiplier.Value);
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
