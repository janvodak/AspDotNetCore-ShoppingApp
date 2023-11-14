using Shopping.WebApp.Features;
using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services
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
			HttpResponseMessage response = await this._client.GetAsync($"/Order/{username}");

			return await this._jsonResponseParser.ParseResponse<List<Order>>(response);
		}
	}
}
