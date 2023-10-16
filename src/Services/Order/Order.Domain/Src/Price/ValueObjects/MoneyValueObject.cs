using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Price.ValueObjects
{
	public class MoneyValueObject : ValueObjectBase
	{
		public int Amount { get; private set; }
		public CurrencyValueObject Currency { get; private set; }

		public MoneyValueObject(int amount, CurrencyValueObject currency)
		{
			this.Amount = amount;
			this.Currency = currency;
		}

		public MoneyValueObject Add(MoneyValueObject money)
		{
			ValidateFitness(money);
			return new MoneyValueObject(Amount + money.Amount, Currency);
		}

		public MoneyValueObject Subtract(MoneyValueObject money)
		{
			ValidateFitness(money);
			return new MoneyValueObject(Amount - money.Amount, Currency);
		}

		private void ValidateFitness(MoneyValueObject money)
		{
			if (Currency.Equals(money.Currency) == false)
			{
				throw new InvalidOperationException(
					string.Format(
						"Different currencies '{0}' and '{1}' for calculation given.",
						this.Currency.Name,
						money.Currency.Name));
			}
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Amount;
			yield return this.Currency;
		}
	}
}
