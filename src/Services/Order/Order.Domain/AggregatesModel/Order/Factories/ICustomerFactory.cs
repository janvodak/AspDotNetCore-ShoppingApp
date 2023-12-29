using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories
{
	public interface ICustomerFactory
	{
		CustomerValueObject Create(string userName);
	}
}