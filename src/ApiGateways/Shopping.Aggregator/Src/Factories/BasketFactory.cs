using System;
using Shopping.Aggregator.Src.Models;
using Shopping.Aggregator.Src.Services;

namespace Shopping.Aggregator.Src.Factories
{
	public class BasketFactory
	{
		private readonly IBasketApiService _basketApiService;
		private readonly IProductApiService _productApiService;

		public BasketFactory(
			IBasketApiService basketApiService,
			IProductApiService productApiService)
		{
			this._basketApiService = basketApiService;
			this._productApiService = productApiService;
		}

		public async Task<Basket> Create(string userName)
		{
			Basket basket = await this._basketApiService.GetBasket(userName);

			foreach (BasketProduct basketProduct in basket.Products)
			{
				Product product = await this._productApiService.GetProductById(basketProduct.Id);

				basketProduct.Name = product.Name;
				basketProduct.Category = product.Category;
				basketProduct.Summary = product.Summary;
				basketProduct.Description = product.Description;
				basketProduct.ImageFile = product.ImageFile;
			}

			return basket;
		}
	}
}
