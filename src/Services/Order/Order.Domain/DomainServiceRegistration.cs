using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment;

namespace ShoppingApp.Services.Order.API.Domain
{
	public static class DomainServiceRegistration
	{
		public static IServiceCollection AddDomainServices(this IServiceCollection services)
		{
			services.AddScoped<IAddressFactory, AddressFactory>();
			services.AddScoped<ICustomerFactory, CustomerFactory>();
			services.AddScoped<IOrderFactory, OrderFactory>();

			services.AddScoped<IPaymentCardFactory, PaymentCardFactory>();

			return services;
		}
	}
}
