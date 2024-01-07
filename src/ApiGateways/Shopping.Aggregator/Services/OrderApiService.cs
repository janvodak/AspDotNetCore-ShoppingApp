using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Factories;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public class OrderApiService : IOrderApiService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
		private readonly IResponseFactory _responseFactory;

		public OrderApiService(
			HttpClient httpClient,
			IHttpRequestMessageFactory httpRequestMessageFactory,
			IResponseFactory responseFactory)
		{
			_httpClient = httpClient;
			_httpRequestMessageFactory = httpRequestMessageFactory;
			_responseFactory = responseFactory;
		}

		public async Task<ResponseDataTransferObject<IEnumerable<OrderDataTransferObject>>> GetUserOrdersAsync(string username)
		{
			Uri clientBaseAddress = _httpClient.BaseAddress
				?? throw new ApplicationException("Unable to send client request due problem with external request.");

			UriBuilder uriBuilder = new(
				clientBaseAddress.Scheme,
				clientBaseAddress.Host,
				clientBaseAddress.Port,
				$"/api/v1/Order/{username}");

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

			return await _responseFactory.Create<ResponseDataTransferObject<IEnumerable<OrderDataTransferObject>>>(httpResponseMessage);
		}
	}
}
