using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories
{
	public interface IAddressFactory
	{
		AddressValueObject Create(
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode);
	}
}