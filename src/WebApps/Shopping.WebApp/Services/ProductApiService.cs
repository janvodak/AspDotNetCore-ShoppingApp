using Shopping.WebApp.Features;
using Shopping.WebApp.Models;
using Shopping.WebApp.Models.DataTransferObjects;

namespace Shopping.WebApp.Services
{
	public class ProductApiService : IProductApiService
	{
		private readonly HttpClient _client;
		private readonly JsonResponseParser _jsonResponseParser;
		private readonly JsonRequestFactory _jsonRequestFactory;
		private readonly ILogger<ProductApiService> _logger;

		public ProductApiService(
			HttpClient client,
			JsonResponseParser jsonResponseParser,
			JsonRequestFactory jsonRequestFactory,
			ILogger<ProductApiService> logger)
		{
			_client = client;
			_jsonResponseParser = jsonResponseParser;
			_jsonRequestFactory = jsonRequestFactory;
			_logger = logger;
		}

		public async Task<Product?> CreateProduct(Product product)
		{
			HttpResponseMessage httpResponseMessage;
			StringContent content = _jsonRequestFactory.CreateNewApiRequest(product);

			try
			{
				httpResponseMessage = await _client.PostAsync("/Product", content);
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to send client request due to reason: '{Message}'", ex.Message);

				return null;
			}

			ResponseDataTransferObject<Product> responseDataTransferObject = await _jsonResponseParser.ParseResponse<ResponseDataTransferObject<Product>>(httpResponseMessage);

			if (responseDataTransferObject.IsSuccess == false || responseDataTransferObject.Result == null)
			{
				_logger.LogError("Unable to parse product response.");

				return null;
			}

			return responseDataTransferObject.Result;
		}

		public async Task<Product?> GetProductById(string id)
		{
			HttpResponseMessage httpResponseMessage;

			try
			{
				httpResponseMessage = await _client.GetAsync($"/Product/{id}");
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to send client request due to reason: '{Message}'", ex.Message);

				return null;
			}

			ResponseDataTransferObject<Product> responseDataTransferObject = await _jsonResponseParser.ParseResponse<ResponseDataTransferObject<Product>>(httpResponseMessage);

			if (responseDataTransferObject.IsSuccess == false || responseDataTransferObject.Result == null)
			{
				_logger.LogError("Unable to parse product response.");

				return null;
			}

			return responseDataTransferObject.Result;
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			HttpResponseMessage httpResponseMessage;

			try
			{
				httpResponseMessage = await _client.GetAsync("/Product");
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to send client request due to reason: '{Message}'", ex.Message);

				return new List<Product>();
			}

			ResponseDataTransferObject<IEnumerable<Product>> responseDataTransferObject = await _jsonResponseParser.ParseResponse<ResponseDataTransferObject<IEnumerable<Product>>>(httpResponseMessage);

			if (responseDataTransferObject.IsSuccess == false || responseDataTransferObject.Result == null)
			{
				_logger.LogError("Unable to parse product response.");

				return new List<Product>();
			}

			return responseDataTransferObject.Result;
		}

		public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
		{
			HttpResponseMessage httpResponseMessage;

			try
			{
				httpResponseMessage = await _client.GetAsync($"/Product/{category}");
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to send client request due to reason: '{Message}'", ex.Message);

				return new List<Product>();
			}

			ResponseDataTransferObject<IEnumerable<Product>> responseDataTransferObject = await _jsonResponseParser.ParseResponse<ResponseDataTransferObject<IEnumerable<Product>>>(httpResponseMessage);

			if (responseDataTransferObject.IsSuccess == false || responseDataTransferObject.Result == null)
			{
				_logger.LogError("Unable to parse product response.");

				return new List<Product>();
			}

			return responseDataTransferObject.Result;
		}
	}
}
