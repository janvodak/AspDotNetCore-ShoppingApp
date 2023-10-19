using Microsoft.Extensions.Logging;
using Order.Domain.Src.Order.Entities;

namespace Order.Infrastructure.Src.Persistence.Context
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
				new OrderEntity()
				{
					UserName = "swn",
					FirstName = "Jan",
					LastName = "Vodak",
					EmailAddress = "janvodak92@gmail.com",
					AddressLine = "Brno",
					Country = "Czech republic",
					TotalPrice = 350
				}
			};
		}
	}
}
