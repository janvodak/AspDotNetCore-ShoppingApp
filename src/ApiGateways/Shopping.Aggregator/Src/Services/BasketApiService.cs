using Shopping.Aggregator.Src.Features;
using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Services
{
	public class BasketApiService : IBasketApiService
	{
		private readonly IBaseService _baseService;
		private readonly IResponseFactory _responseFactory;

		public BasketApiService(
			IBaseService baseService,
			IResponseFactory responseFactory)
		{
			_baseService = baseService;
			_responseFactory = responseFactory;
		}

		public async Task<BasketDataTransferObject> GetBasketAsync(string username)
		{
			RequestDataTransferObject request = new($"/api/v1/basket/GetBasket/{username}");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("BasketApi", request);

			return await _responseFactory.Create<BasketDataTransferObject>(httpResponseMessage);
		}
	}
}
