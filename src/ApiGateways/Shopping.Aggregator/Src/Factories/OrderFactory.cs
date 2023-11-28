using System.Collections.Generic;
using Shopping.Aggregator.Src.Models;
using Shopping.Aggregator.Src.Services;

namespace Shopping.Aggregator.Src.Factories
{
	public class OrderFactory
	{
		private readonly IOrderApiService _orderApiService;
		private readonly ILogger<OrderFactory> _logger;

		public OrderFactory(
			IOrderApiService orderApiService,
			ILogger<OrderFactory> logger)
		{
			_orderApiService = orderApiService;
			_logger = logger;
		}

		public async Task<IEnumerable<Order>> Create(string userName)
		{
			try
			{
				return await _orderApiService.GetUserOrdersAsync(userName);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Unable to get orders for user '{UserName}'", userName);
			}

			return new List<Order>();
		}
	}
}
