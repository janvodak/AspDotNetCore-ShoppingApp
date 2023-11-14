using Shopping.WebApp.Features;
using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services
{
	public class ProductApiService : IProductApiService
	{
		private readonly HttpClient _client;
		private readonly JsonResponseParser _jsonResponseParser;
		private readonly JsonRequestFactory _jsonRequestFactory;

		public ProductApiService(
			HttpClient client,
			JsonResponseParser jsonResponseParser,
			JsonRequestFactory jsonRequestFactory)
		{
			this._client = client;
			this._jsonResponseParser = jsonResponseParser;
			this._jsonRequestFactory = jsonRequestFactory;
		}

		public async Task<Product> CreateProduct(Product product)
		{
			StringContent content = this._jsonRequestFactory.CreateNewApiRequest<Product>(product);

			HttpResponseMessage response = await this._client.PostAsync("/Catalog", content);

			return await this._jsonResponseParser.ParseResponse<Product>(response);
		}

		public async Task<Product> GetProductById(string id)
		{
			HttpResponseMessage response = await this._client.GetAsync($"/Product/{id}");

			return await this._jsonResponseParser.ParseResponse<Product>(response);
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			HttpResponseMessage response = await this._client.GetAsync("/Product");

			return await this._jsonResponseParser.ParseResponse<List<Product>>(response);
		}

		public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
		{
			HttpResponseMessage response = await this._client.GetAsync($"/Product/{category}");

			return await this._jsonResponseParser.ParseResponse<List<Product>>(response);
		}
	}
}
