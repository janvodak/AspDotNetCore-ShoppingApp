using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;

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
			return new List<OrderAggregateRoot>
			{
				//new OrderAggregateRoot(
				//	"swn",
				//	350,
				//	"Jan",
				//	"Vodak",
				//	"janvodak92@gmail.com",
				//	"Brno",
				//	"Czech republic",
				//	"Czech republic",
				//	"60200",
				//	"Test Card",
				//	"5555555555554444",
				//	"03/2030",
				//	"737",
				//	1
				//)
			};
		}
	}
}
