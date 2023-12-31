﻿using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment
{
	public class PaymentCardValueObject : ValueObjectBase
	{
		public PaymentCardValueObject(
			string cardName,
			string cardNumber,
			string expiration,
			string cardVerificationValue)
		{
			CardName = cardName;
			CardNumber = cardNumber;
			Expiration = expiration;
			CardVerificationValue = cardVerificationValue;
		}

		public string CardName { get; private set; }
		public string CardNumber { get; private set; }
		public string Expiration { get; private set; }
		public string CardVerificationValue { get; private set; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return CardName;
			yield return CardNumber;
			yield return Expiration;
			yield return CardVerificationValue;
		}
	}
}
