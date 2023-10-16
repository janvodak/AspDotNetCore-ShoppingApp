using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Price.ValueObjects
{
	public class PriceValueObject : ValueObjectBase
	{
		public MoneyValueObject MoneyWithVat { get; private set; }
		public VatRateValueObject VatRate { get; private set; }

		public PriceValueObject(MoneyValueObject moneyWithVat, VatRateValueObject vatRate)
		{
			this.MoneyWithVat = moneyWithVat;
			this.VatRate = vatRate;
		}

		public int GetAmountWithVat()
		{
			return this.MoneyWithVat.Amount;
		}

		public CurrencyValueObject GetCurrency()
		{
			return this.MoneyWithVat.Currency;
		}

		public int GetVatAmount()
		{
			return this.MoneyWithVat.Amount - this.GetAmountWithoutVat();
		}

		public int GetAmountWithoutVat()
		{
			return (int)Math.Round(this.MoneyWithVat.Amount / this.VatRate.GetVatCoefficient());
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
			this.ValidateFitness(price);

			MoneyValueObject money = this.MoneyWithVat.Add(price.MoneyWithVat);

			return new(money, this.VatRate);
		}

		public PriceValueObject Subtract(PriceValueObject price)
		{
			this.ValidateFitness(price);

			MoneyValueObject money = this.MoneyWithVat.Subtract(price.MoneyWithVat);

			return new(money, this.VatRate);
		}

		public PriceValueObject Multiply(QuantityValueObject multiplier)
		{
			MoneyValueObject money = new(this.MoneyWithVat.Amount * multiplier.Value, this.MoneyWithVat.Currency);

			return new(money, this.VatRate);
		}

		private void ValidateFitness(PriceValueObject price)
		{
			if (this.VatRate.Equals(price.VatRate) == false)
			{
				string message = $"Different VAT rates for calculation. VatRate: {this.VatRate.Value}, Other VatRate: {price.VatRate.Value}";
				throw new InvalidOperationException(message);
			}
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.MoneyWithVat;
			yield return this.VatRate;
		}
	}
}
