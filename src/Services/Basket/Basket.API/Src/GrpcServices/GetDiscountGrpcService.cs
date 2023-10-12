
using Discount.Grpc.Src.Protos;
using Grpc.Core;

namespace Basket.API.Src.GrpcServices
{
	public class GetDiscountGrpcService
	{
		private readonly ILogger<GetDiscountGrpcService> _logger;
		private readonly GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceClient _getDiscountProtocolBufferService;

		public GetDiscountGrpcService(
			ILogger<GetDiscountGrpcService> logger,
			GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceClient getDiscountProtocolBufferService)
		{
			_logger = logger;
			this._getDiscountProtocolBufferService = getDiscountProtocolBufferService;
		}

		public async Task<GetDiscountProtocolBufferEntity?> GetDiscount(string productName)
		{
			GetDiscountRequest discountRequest = new() { ProductName = productName };

			try
			{
				return await this._getDiscountProtocolBufferService.GetDiscountAsync(discountRequest);
			}
			catch (RpcException exception)
			{
				if (exception.Status.StatusCode == StatusCode.NotFound)
				{
					this._logger.LogInformation($"Product '{productName}' has no discounts.");
				}
				else
				{
					this._logger.LogError($"Unable to get discounts for product '{productName}' due to error: '{exception.Message}'");
				}
			}

			return null;
		}
	}
}
