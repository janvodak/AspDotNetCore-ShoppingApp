using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;
using ShoppingApp.ApiGateway.ShoppingAggregator.Services;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers
{
	public class OrderHandler : IOrderHandler
	{
		private readonly IOrderApiService _orderApiService;
		private readonly ILogger<OrderHandler> _logger;

		public OrderHandler(
			IOrderApiService orderApiService,
			ILogger<OrderHandler> logger)
		{
			_orderApiService = orderApiService;
			_logger = logger;
		}

		public async Task<IEnumerable<OrderDataTransferObject>> Handle(string userName)
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
