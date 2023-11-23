using Grpc.Core;
using ShoppingApp.Services.Discount.Grpc.Protos;
using ShoppingApp.Services.Discount.Grpc.Repositories;

namespace ShoppingApp.Services.Discount.Grpc.Services;

public class DeleteDiscoutService : DeleteDiscountProtocolBufferService.DeleteDiscountProtocolBufferServiceBase
{
	private readonly IDiscountRepository _repository;
	private readonly ILogger<DeleteDiscoutService> _logger;

	public DeleteDiscoutService(
		IDiscountRepository discountRepository,
		ILogger<DeleteDiscoutService> logger)
	{
		_repository = discountRepository;
		_logger = logger;
	}

	public override async Task<DeleteDiscountResponse> DeleteDiscount(
		DeleteDiscountRequest request,
		ServerCallContext context)
	{
		int result;

		try
		{
			result = await _repository.DeleteDiscountAsync(request.ProductName);
		}
		catch (Exception)
		{
			_logger.LogError("Unable to remove discount for product '{ProductName}'.",
				request.ProductName);

			Status status = new(
				StatusCode.Internal,
				$"Unable to delete discount for product '{request.ProductName}'.");

			throw new RpcException(status);
		}

		if (result == 0)
		{
			_logger.LogError("Unable to remove discount for product '{ProductName}.'",
				request.ProductName);

			Status status = new(
				StatusCode.Internal,
				$"Unable to delete discount for product '{request.ProductName}'.");

			throw new RpcException(status);
		}

		_logger.LogInformation(
			"Discount for product '{ProductName}' was removed successfully.",
			request.ProductName);

		var response = new DeleteDiscountResponse
		{
			Success = Convert.ToBoolean(result)
		};

		return response;
	}
}
