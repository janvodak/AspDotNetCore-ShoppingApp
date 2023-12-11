using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;
using ShoppingApp.ApiGateway.ShoppingAggregator.Services;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers
{
	public class BasketHandler : IBasketHandler
	{
		private readonly IBasketApiService _basketApiService;
		private readonly IProductHandler _productHandler;
		private readonly ILogger<BasketHandler> _logger;

		public BasketHandler(
			IBasketApiService basketApiService,
			IProductHandler productHandler,
			ILogger<BasketHandler> logger)
		{
			_basketApiService = basketApiService;
			_productHandler = productHandler;
			_logger = logger;
		}

		public async Task<BasketDataTransferObject> Handle(string userName)
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
				ProductDataTransferObject? productDataTransferObject = await _productHandler.Handle(basketProduct.Id);

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
