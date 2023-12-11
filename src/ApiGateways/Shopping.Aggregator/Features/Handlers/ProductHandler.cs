using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;
using ShoppingApp.ApiGateway.ShoppingAggregator.Services;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers
{
	public class ProductHandler : IProductHandler
	{
		private readonly IProductApiService _productApiService;
		private readonly ILogger<BasketHandler> _logger;

		public ProductHandler(
			IProductApiService productApiService,
			ILogger<BasketHandler> logger)
		{
			_productApiService = productApiService;
			_logger = logger;
		}

		public async Task<ProductDataTransferObject?> Handle(string id)
		{
			ResponseDataTransferObject<ProductDataTransferObject> responseDataTransferObject;

			try
			{
				responseDataTransferObject = await _productApiService.GetProductByIdAsync(id);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(
					ex,
					"Unable to get Product '{BasketProductID}'.",
					id);

				return null;
			}

			if (responseDataTransferObject.IsSuccess == false || responseDataTransferObject.Result == null)
			{
				_logger.LogWarning(
					"Unable to parse response for product '{BasketProductID}'.",
					id);

				return null;
			}

			return responseDataTransferObject.Result;
		}
	}
}
