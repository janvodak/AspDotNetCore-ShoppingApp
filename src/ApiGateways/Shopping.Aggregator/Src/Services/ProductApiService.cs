using Shopping.Aggregator.Src.Features;
using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Services
{
	public class ProductApiService : IProductApiService
	{
		private readonly HttpClient _client;
		private readonly JsonResponseParser _jsonResponseParser;

		public ProductApiService(
			HttpClient client,
			JsonResponseParser jsonResponseParser)
		{
			this._client = client;
			this._jsonResponseParser = jsonResponseParser;
		}

		public async Task<Product> GetProductById(string id)
		{
			HttpResponseMessage response = await this._client.GetAsync($"/api/v1/catalog/GetProductById/{id}");

			return await this._jsonResponseParser.Parse<Product>(response);
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			HttpResponseMessage response = await this._client.GetAsync("/api/v1/catalog/GetProducts");

			return await this._jsonResponseParser.Parse<List<Product>>(response);
		}

		public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
		{
			HttpResponseMessage response = await this._client.GetAsync($"/api/v1/catalog/GetProductsByCategory/{category}");

			return await this._jsonResponseParser.Parse<List<Product>>(response);
		}
	}
}
