using System;
namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment
{
	public class PaymentCardFactory : IPaymentCardFactory
	{
		public PaymentCardValueObject Create(
			string cardName,
			string cardNumber,
			string expiration,
			string cardVerificationValue)
		{
			return new PaymentCardValueObject(
				cardName,
				cardNumber,
				expiration,
				cardVerificationValue);
		}
	}
}

