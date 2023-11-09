using Shopping.Aggregator.Src.Models;
using Shopping.Aggregator.Src.Services;

namespace Shopping.Aggregator.Src.Factories
{
	public class OrderFactory
	{
		private readonly IOrderApiService _orderApiService;

		public OrderFactory(IOrderApiService orderApiService)
		{
			this._orderApiService = orderApiService;
		}

		public async Task<IEnumerable<Order>> Create(string userName)
		{
			IEnumerable<Order> orders = await this._orderApiService.GetUserOrders(userName);

			return orders;
		}
	}
}
