using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Domain.Order;

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

		private static IEnumerable<OrderEntity> GetPreconfiguredOrders()
		{
			return new List<OrderEntity>
			{
				new OrderEntity(
					"swn",
					350,
					"Jan",
					"Vodak",
					"janvodak92@gmail.com",
					"Brno",
					"Czech republic",
					"Czech republic",
					"60200",
					"Test Card",
					"5555555555554444",
					"03/2030",
					"737",
					1
				)
			};
		}
	}
}
