namespace Order.Domain.Src.Shared
{
	public class QuantityValueObject : ValueObjectBase
	{
		public int Value { get; private set; }

		public QuantityValueObject(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(
					nameof(value),
					"Quantity must be greater than or equal to 0.");
			}

			this.Value = value;
		}

		public QuantityValueObject Increase(QuantityValueObject increment)
		{
			return new QuantityValueObject(this.Value + increment.Value);
		}

		public QuantityValueObject Decrease(QuantityValueObject decrement)
		{
			return new QuantityValueObject(this.Value - decrement.Value);
		}

		public QuantityValueObject Multiply(QuantityValueObject multiplier)
		{
			return new QuantityValueObject(this.Value * multiplier.Value);
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Value;
		}
	}
}
