using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Payment.ValueObjects
{
	public class PaymentCardValueObject : ValueObjectBase
	{
		public string CardName { get; private set; }
		public string CardNumber { get; private set; }
		public string Expiration { get; private set; }
		public string CardVerificationValue { get; set; }

		public PaymentCardValueObject(string cardName, string cardNumber, string expiration, string cardVerificationValue)
		{
			this.CardName = cardName;
			this.CardNumber = cardNumber;
			this.Expiration = expiration;
			this.CardVerificationValue = cardVerificationValue;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.CardName;
			yield return this.CardNumber;
			yield return this.Expiration;
			yield return this.CardVerificationValue;
		}
	}
}
