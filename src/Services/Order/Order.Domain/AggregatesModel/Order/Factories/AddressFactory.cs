using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories
{
	public class AddressFactory
	{
		public AddressValueObject Create(
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode)
		{
			return new AddressValueObject(
				firstName,
				lastName,
				emailAddress,
				addressLine,
				country,
				state,
				zipCode);
		}
	}
}
