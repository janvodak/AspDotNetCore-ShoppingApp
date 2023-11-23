using Grpc.Core;
using ShoppingApp.Services.Discount.Grpc.Protos;
using ShoppingApp.Services.Discount.Grpc.Repositories;

namespace ShoppingApp.Services.Discount.Grpc.Services;

public class DeleteDiscoutService : DeleteDiscountProtocolBufferService.DeleteDiscountProtocolBufferServiceBase
{
	private readonly IDiscountRepository _repository;

	public DeleteDiscoutService(IDiscountRepository discountRepository)
	{
		_repository = discountRepository;
	}

	public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
	{
		bool isDeleted = await _repository.DeleteDiscount(request.ProductName);

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
