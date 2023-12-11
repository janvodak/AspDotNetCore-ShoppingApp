using ShoppingApp.ApiGateway.ShoppingAggregator.Services;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects.Factories
{
	public class BasketFactory : IBasketFactory
	{
		private readonly IBasketApiService _basketApiService;
		private readonly IProductFactory _productFactory;
		private readonly ILogger<BasketFactory> _logger;

		public BasketFactory(
			IBasketApiService basketApiService,
			IProductFactory productFactory,
			ILogger<BasketFactory> logger)
		{
			_basketApiService = basketApiService;
			_productFactory = productFactory;
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
				ProductDataTransferObject? productDataTransferObject = await _productFactory.Create(basketProduct.Id);

				if (productDataTransferObject == null)
				{
					break;
				}

				basketProduct.Name = productDataTransferObject.Name;
				basketProduct.Category = productDataTransferObject.Category;
				basketProduct.Summary = productDataTransferObject.Summary;
				basketProduct.Description = productDataTransferObject.Description;
				basketProduct.ImageFile = productDataTransferObject.ImageFile;
			}

			return basket;
		}
	}
}
