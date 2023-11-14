using Shopping.WebApp.Features;
using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services
{
	public class BasketApiService : IBasketApiService
	{
		private readonly HttpClient _client;
		private readonly JsonResponseParser _jsonResponseParser;
		private readonly JsonRequestFactory _jsonRequestFactory;

		public BasketApiService(
			HttpClient client,
			JsonResponseParser jsonResponseParser,
			JsonRequestFactory jsonRequestFactory)
		{
			this._client = client;
			this._jsonResponseParser = jsonResponseParser;
			this._jsonRequestFactory = jsonRequestFactory;
		}

		public async Task CheckoutBasket(CheckoutBasket basket)
		{
			StringContent content = this._jsonRequestFactory.CreateNewApiRequest<CheckoutBasket>(basket);

			HttpResponseMessage response = await this._client.PostAsync("/Basket/Checkout", content);

			this._jsonResponseParser.ValidateResponse(response);
		}

		public async Task<Basket> GetBasket(string username)
		{
			HttpResponseMessage response = await this._client.GetAsync($"/Basket/{username}");

			return await this._jsonResponseParser.ParseResponse<Basket>(response);
		}

		public async Task<Basket> UpdateBasket(Basket basket)
		{
			StringContent content = this._jsonRequestFactory.CreateNewApiRequest<Basket>(basket);

			HttpResponseMessage response = await this._client.PostAsync("/Basket", content);

			return await this._jsonResponseParser.ParseResponse<Basket>(response);
		}
	}
}
