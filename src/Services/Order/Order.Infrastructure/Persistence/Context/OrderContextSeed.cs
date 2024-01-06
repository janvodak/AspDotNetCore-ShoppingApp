using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context
{
	public class OrderContextSeed
	{
		public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
		{
			if (orderContext.Orders.Any() == false)
			{
				orderContext.Orders.AddRange(GetPreconfiguredOrders());

				await orderContext.SaveChangesAsync();

				logger.LogInformation(
					"Seed database associated with context {DbContextName}",
					typeof(OrderContext).Name);
			}
		}

		private static IEnumerable<OrderAggregateRoot> GetPreconfiguredOrders()
		{
			CustomerValueObject customer = new("swn");

			PriceValueObject price = PriceValueObject.FromFloat(
				350,
				CurrencyEnumeration.GBP,
				new VatRateValueObject(20));

			AddressValueObject billingAddress = new(
				"Jan",
				"Vodak",
				"admin@admin.com",
				"Brno",
				"Czech republic",
				"Czech republic",
				"60200");

			PaymentCardValueObject paymentCard = new(
				"Test Card",
				"5555555555554444",
				"03/2030",
				"737");

			PaymentMethodEnumeration paymentMethod = PaymentMethodEnumeration.Card;

			return new List<OrderAggregateRoot>
			{
				new OrderAggregateRoot(
					customer,
					price,
					billingAddress,
					paymentCard,
					paymentMethod)
			};
		}
	}
}
