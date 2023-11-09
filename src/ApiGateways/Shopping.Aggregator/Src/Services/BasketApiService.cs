using Shopping.Aggregator.Src.Features;
using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Services
{
	public class BasketApiService : IBasketApiService
	{
		private readonly HttpClient _client;
		private readonly JsonResponseParser _jsonResponseParser;

		public BasketApiService(
			HttpClient client,
			JsonResponseParser jsonResponseParser)
		{
			this._client = client;
			this._jsonResponseParser = jsonResponseParser;
		}

		public async Task<Basket> GetBasket(string username)
		{
			HttpResponseMessage response = await this._client.GetAsync($"/api/v1/basket/GetBasket/{username}");

			return await this._jsonResponseParser.Parse<Basket>(response);
		}
	}
}
