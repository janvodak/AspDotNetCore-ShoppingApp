using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Factories;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public class BasketApiService : IBasketApiService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
		private readonly IResponseFactory _responseFactory;

		public BasketApiService(
			HttpClient client,
			IHttpRequestMessageFactory httpRequestMessageFactory,
			IResponseFactory responseFactory)
		{
			_httpClient = client;
			_httpRequestMessageFactory = httpRequestMessageFactory;
			_responseFactory = responseFactory;
		}

		public async Task<BasketDataTransferObject> GetBasketAsync(string username)
		{
			Uri clientBaseAddress = _httpClient.BaseAddress
				?? throw new ApplicationException("Unable to send client request due problem with external request.");

			UriBuilder uriBuilder = new(
				clientBaseAddress.Scheme,
				clientBaseAddress.Host,
				clientBaseAddress.Port,
				$"/api/v1/Basket/GetBasket/{username}");

			RequestDataTransferObject request = new(uriBuilder.Uri.OriginalString);
			HttpRequestMessage httpRequestMessage = _httpRequestMessageFactory.Create(request);
			HttpResponseMessage httpResponseMessage;

			try
			{
				httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Unable to send client request due to reason: '{ex.Message}'", ex);
			}

			return await _responseFactory.Create<BasketDataTransferObject>(httpResponseMessage);
		}
	}
}
