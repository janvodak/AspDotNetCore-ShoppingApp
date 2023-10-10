using Discount.Grpc.Src.Protos;
using Discount.Grpc.Src.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Src.Services;

public class DeleteDiscoutService : DeleteDiscountProtocolBufferService.DeleteDiscountProtocolBufferServiceBase
{
	private readonly IDiscountRepository _repository;

	public DeleteDiscoutService(IDiscountRepository discountRepository)
	{
		this._repository = discountRepository;
	}

	public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
	{
		bool isDeleted = await this._repository.DeleteDiscount(request.ProductName);

		if (isDeleted == false)
		{
			string errorResponseMessage = $"Unable to delete discount for product '{request.ProductName}'.";
			throw new RpcException(new Status(StatusCode.Internal, errorResponseMessage));
		}

		var response = new DeleteDiscountResponse
		{
			Success = isDeleted
		};

		return response;
	}
}
