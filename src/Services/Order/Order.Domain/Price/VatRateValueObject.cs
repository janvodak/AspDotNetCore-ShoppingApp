using ShoppingApp.Services.Order.API.Domain.Shared;

namespace ShoppingApp.Services.Order.API.Domain.Price
{
	public class VatRateValueObject : ValueObjectBase
	{
		public VatRateValueObject(int value)
		{
			if (value < 0 || value > 100)
			{
				throw new ArgumentOutOfRangeException(
					nameof(value),
					"Invalid VAT rate value. VAT rate must be between 0 and 100.");
			}

			Value = value;
		}

		public int Value { get; private set; }

		public decimal GetVatCoefficient()
		{
			return 1 + (decimal)Value / 100;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
