using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories
{
	public class OrderFactory : IOrderFactory
	{
		private readonly CustomerFactory _customerFactory;
		private readonly AddressFactory _addressFactory;
		private readonly PaymentCardFactory _paymentCardFactory;

		public OrderFactory(
			CustomerFactory customerFactory,
			AddressFactory addressFactory,
			PaymentCardFactory paymentCardFactory)
		{
			_customerFactory = customerFactory;
			_addressFactory = addressFactory;
			_paymentCardFactory = paymentCardFactory;
		}

		public OrderAggregateRoot Create(
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
			string cardVerificationValue)
		{
			CustomerValueObject customer = _customerFactory.Create(userName);
			AddressValueObject address = _addressFactory.Create(
				firstName,
				lastName,
				emailAddress,
				addressLine,
				country,
				state,
				zipCode);

			PriceValueObject price = PriceValueObject.FromFloat(
				totalPrice,
				CurrencyEnumeration.EUR,
				new VatRateValueObject(20));

			PaymentCardValueObject paymentCard = _paymentCardFactory.Create(
				cardName,
				cardNumber,
				expiration,
				cardVerificationValue);

			PaymentMethodEnumeration paymentMethodEnumeration = PaymentMethodEnumeration.Card;

			return new OrderAggregateRoot(
				customer,
				price,
				address,
				paymentCard,
				paymentMethodEnumeration);
		}
	}
}
