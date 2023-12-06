using ShoppingApp.ApiGateway.ShoppingAggregator.Features;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public class ProductApiService : IProductApiService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
		private readonly IResponseFactory _responseFactory;

		public ProductApiService(
			HttpClient httpClient,
			IHttpRequestMessageFactory httpRequestMessageFactory,
			IResponseFactory responseFactory)
		{
			_httpClient = httpClient;
			_httpRequestMessageFactory = httpRequestMessageFactory;
			_responseFactory = responseFactory;
		}

		public async Task<ProductDataTransferObject> GetProductByIdAsync(string id)
		{
			Uri clientBaseAddress = _httpClient.BaseAddress
				?? throw new ApplicationException("Unable to send client request due problem with external request.");

			UriBuilder uriBuilder = new(
				clientBaseAddress.Scheme,
				clientBaseAddress.Host,
				clientBaseAddress.Port,
				$"/api/v1/Catalog/GetProductById/{id}");

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

			return await _responseFactory.Create<ProductDataTransferObject>(httpResponseMessage);
		}

		public async Task<IEnumerable<ProductDataTransferObject>> GetProductsAsync()
		{
			Uri clientBaseAddress = _httpClient.BaseAddress
				?? throw new ApplicationException("Unable to send client request due problem with external request.");

			UriBuilder uriBuilder = new(
				clientBaseAddress.Scheme,
				clientBaseAddress.Host,
				clientBaseAddress.Port,
				$"/api/v1/Catalog/GetProducts");

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

			return await _responseFactory.Create<IEnumerable<ProductDataTransferObject>>(httpResponseMessage);
		}

		public async Task<IEnumerable<ProductDataTransferObject>> GetProductsByCategoryAsync(string category)
		{
			Uri clientBaseAddress = _httpClient.BaseAddress
				?? throw new ApplicationException("Unable to send client request due problem with external request.");

			UriBuilder uriBuilder = new(
				clientBaseAddress.Scheme,
				clientBaseAddress.Host,
				clientBaseAddress.Port,
				$"/api/v1/Catalog/GetProductsByCategory/{category}");

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

			return await _responseFactory.Create<IEnumerable<ProductDataTransferObject>>(httpResponseMessage);
		}
	}
}
