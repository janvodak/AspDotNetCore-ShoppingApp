using ShoppingApp.ApiGateway.ShoppingAggregator.Features;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public class ProductApiService : IProductApiService
	{
		private readonly IBaseService _baseService;
		private readonly IResponseFactory _responseFactory;

		public ProductApiService(
			IBaseService baseService,
			IResponseFactory responseFactory)
		{
			_baseService = baseService;
			_responseFactory = responseFactory;
		}

		public async Task<ProductDataTransferObject> GetProductByIdAsync(string id)
		{
			RequestDataTransferObject request = new($"/api/v1/catalog/GetProductById/{id}");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("ProductApi", request);

			return await _responseFactory.Create<ProductDataTransferObject>(httpResponseMessage);
		}

		public async Task<IEnumerable<ProductDataTransferObject>> GetProductsAsync()
		{
			RequestDataTransferObject request = new("/api/v1/catalog/GetProducts");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("ProductApi", request);

			return await _responseFactory.Create<IEnumerable<ProductDataTransferObject>>(httpResponseMessage);
		}

		public async Task<IEnumerable<ProductDataTransferObject>> GetProductsByCategoryAsync(string category)
		{
			var request = new RequestDataTransferObject($"/api/v1/catalog/GetProductsByCategory/{category}");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("ProductApi", request);

			return await _responseFactory.Create<IEnumerable<ProductDataTransferObject>>(httpResponseMessage);
		}
	}
}
