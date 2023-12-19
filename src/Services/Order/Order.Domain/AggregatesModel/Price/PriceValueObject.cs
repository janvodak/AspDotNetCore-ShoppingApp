using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Shared;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price
{
	public class PriceValueObject : ValueObjectBase
	{
		public PriceValueObject(MoneyValueObject moneyWithVat, VatRateValueObject vatRate)
		{
			MoneyWithVat = moneyWithVat;
			VatRate = vatRate;
		}

		public MoneyValueObject MoneyWithVat { get; private set; }
		public VatRateValueObject VatRate { get; private set; }

		public int GetAmountWithVat()
		{
			return MoneyWithVat.Amount;
		}

		public CurrencyValueObject GetCurrency()
		{
			return MoneyWithVat.Currency;
		}

		public int GetVatAmount()
		{
			return MoneyWithVat.Amount - GetAmountWithoutVat();
		}

		public int GetAmountWithoutVat()
		{
			return (int)Math.Round(MoneyWithVat.Amount / VatRate.GetVatCoefficient());
		}

		public static PriceValueObject FromFloat(decimal price, CurrencyValueObject currency, VatRateValueObject vatRate)
		{
			decimal roundedPrice = Math.Round(price, 2);
			int amountWithVat = (int)Math.Round(roundedPrice * 100);

			MoneyValueObject money = new(amountWithVat, currency);

			return new(money, vatRate);
		}

		public PriceValueObject Add(PriceValueObject price)
		{
			ValidateFitness(price);

			MoneyValueObject money = MoneyWithVat.Add(price.MoneyWithVat);

			return new(money, VatRate);
		}

		public PriceValueObject Subtract(PriceValueObject price)
		{
			ValidateFitness(price);

			MoneyValueObject money = MoneyWithVat.Subtract(price.MoneyWithVat);

			return new(money, VatRate);
		}

		public PriceValueObject Multiply(QuantityValueObject multiplier)
		{
			MoneyValueObject money = new(MoneyWithVat.Amount * multiplier.Value, MoneyWithVat.Currency);

			return new(money, VatRate);
		}

		private void ValidateFitness(PriceValueObject price)
		{
			if (VatRate.Equals(price.VatRate) == false)
			{
				string message = $"Different VAT rates for calculation. VatRate: {VatRate.Value}, Other VatRate: {price.VatRate.Value}";
				throw new InvalidOperationException(message);
			}
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return MoneyWithVat;
			yield return VatRate;
		}
	}
}
