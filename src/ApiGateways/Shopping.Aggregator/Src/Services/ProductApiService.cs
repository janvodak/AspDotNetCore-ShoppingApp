﻿using Shopping.Aggregator.Src.Features;
using Shopping.Aggregator.Src.Models;
using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Services
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

		public async Task<Product> GetProductByIdAsync(string id)
		{
			RequestDataTransferObject request = new($"/api/v1/catalog/GetProductById/{id}");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("ProductApi", request);

			return await _responseFactory.Create<Product>(httpResponseMessage);
		}

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			RequestDataTransferObject request = new("/api/v1/catalog/GetProducts");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("ProductApi", request);

			return await _responseFactory.Create<IEnumerable<Product>>(httpResponseMessage);
		}

		public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
		{
			var request = new RequestDataTransferObject($"/api/v1/catalog/GetProductsByCategory/{category}");

			HttpResponseMessage httpResponseMessage = await _baseService.SendAsync("ProductApi", request);

			return await _responseFactory.Create<IEnumerable<Product>>(httpResponseMessage);
		}
	}
}