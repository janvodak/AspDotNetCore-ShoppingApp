using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories
{
	public interface IOrderFactory
	{
		OrderAggregateRoot Create(
			string userName,
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode,
			decimal totalPrice,
			int paymentMethod,
			string cardName,
			string cardNumber,
			string expiration,
			string cardVerificationValue);
	}
}