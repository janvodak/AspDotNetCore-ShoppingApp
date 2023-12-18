using ShoppingApp.Services.Order.API.Domain.Shared;

namespace ShoppingApp.Services.Order.API.Domain.Price
{
	public class MoneyValueObject : ValueObjectBase
	{
		public MoneyValueObject(int amount, CurrencyValueObject currency)
		{
			Amount = amount;
			Currency = currency;
		}

		public int Amount { get; private set; }
		public CurrencyValueObject Currency { get; private set; }

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
						Currency.Name,
						money.Currency.Name));
			}
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Amount;
			yield return Currency;
		}
	}
}
