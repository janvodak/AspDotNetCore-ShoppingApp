using ShoppingApp.ApiGateway.ShoppingAggregator.Features;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public class OrderApiService : IOrderApiService
	{
		private readonly IBaseService _baseService;
		private readonly IResponseFactory _responseFactory;

		public OrderApiService(
			IBaseService baseService,
			IResponseFactory responseFactory)
		{
			_baseService = baseService;
			_responseFactory = responseFactory;
		}

		public async Task<IEnumerable<OrderDataTransferObject>> GetUserOrdersAsync(string username)
		{
			RequestDataTransferObject request = new($"/api/v1/order/GetUserOrders/{username}");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("OrderApi", request);

			return await _responseFactory.Create<IEnumerable<OrderDataTransferObject>>(httpResponseMessage);
		}
	}
}
