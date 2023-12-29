namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment
{
	public interface IPaymentCardFactory
	{
		PaymentCardValueObject Create(
			string cardName,
			string cardNumber,
			string expiration,
			string cardVerificationValue);
	}
}