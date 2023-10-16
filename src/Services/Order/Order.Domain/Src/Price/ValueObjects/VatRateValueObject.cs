using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Price.ValueObjects
{
	public class VatRateValueObject : ValueObjectBase
	{
		public int Value { get; private set; }

		public VatRateValueObject(int value)
		{
			if (value < 0 || value > 100)
			{
				throw new ArgumentOutOfRangeException(
					nameof(value),
					"Invalid VAT rate value. VAT rate must be between 0 and 100.");
			}

			this.Value = value;
		}

		public decimal GetVatCoefficient()
		{
			return 1 + (decimal)this.Value / 100;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Value;
		}
	}
}
