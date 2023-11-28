using Shopping.Aggregator.Src.Services;

namespace Shopping.Aggregator.Src.Models.DataTransferObjects.Factories
{
	public class BasketFactory : IBasketFactory
	{
		private readonly IBasketApiService _basketApiService;
		private readonly IProductApiService _productApiService;
		private readonly ILogger<BasketFactory> _logger;

		public BasketFactory(
			IBasketApiService basketApiService,
			IProductApiService productApiService,
			ILogger<BasketFactory> logger)
		{
			_basketApiService = basketApiService;
			_productApiService = productApiService;
			_logger = logger;
		}

		public async Task<BasketDataTransferObject> Create(string userName)
		{
			BasketDataTransferObject basket;

			try
			{
				basket = await _basketApiService.GetBasketAsync(userName);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(
					ex,
					"Unable to get Basket for user '{UserName}'",
					userName);

				throw;
			}

			foreach (BasketProductDataTransferObject basketProduct in basket.Products)
			{
				ProductDataTransferObject product;

				try
				{
					product = await _productApiService.GetProductByIdAsync(basketProduct.Id);
				}
				catch (Exception ex)
				{
					_logger.LogWarning(
						ex,
						"Unable to get Product '{BasketProductID}' for user '{UserName}'",
						basketProduct.Id,
						userName);

					break;
				}

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
