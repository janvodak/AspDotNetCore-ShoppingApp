using Shopping.Aggregator.Src.Features;
using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Services
{
	public class OrderApiService : IOrderApiService
	{
		private readonly HttpClient _client;
		private readonly JsonResponseParser _jsonResponseParser;

		public OrderApiService(
			HttpClient client,
			JsonResponseParser jsonResponseParser)
		{
			this._client = client;
			this._jsonResponseParser = jsonResponseParser;
		}

		public async Task<IEnumerable<Order>> GetUserOrders(string username)
		{
			HttpResponseMessage response = await this._client.GetAsync($"/api/v1/order/GetUserOrders/{username}");

			return await this._jsonResponseParser.Parse<List<Order>>(response);
		}
	}
}
