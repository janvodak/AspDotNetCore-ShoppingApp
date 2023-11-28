using ShoppingApp.ApiGateway.ShoppingAggregator.Services;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects.Factories
{
	public class OrderFactory : IOrderFactory
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

		public async Task<IEnumerable<OrderDataTransferObject>> Create(string userName)
		{
			try
			{
				return await _orderApiService.GetUserOrdersAsync(userName);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Unable to get Orders for user '{UserName}'", userName);
			}

			return new List<OrderDataTransferObject>();
		}
	}
}
