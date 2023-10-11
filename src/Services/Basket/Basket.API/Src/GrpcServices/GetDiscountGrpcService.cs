
using Discount.Grpc.Src.Protos;

namespace Basket.API.Src.GrpcServices
{
	public class GetDiscountGrpcService
	{
		private readonly GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceClient _getDiscountProtocolBufferService;

		public GetDiscountGrpcService(GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceClient getDiscountProtocolBufferService)
		{
			this._getDiscountProtocolBufferService = getDiscountProtocolBufferService;
		}

		public async Task<GetDiscountProtocolBufferEntity> GetDiscount(string productName)
		{
			GetDiscountRequest discountRequest = new() { ProductName = productName };

			return await this._getDiscountProtocolBufferService.GetDiscountAsync(discountRequest);
		}
	}
}
