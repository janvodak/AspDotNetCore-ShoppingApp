using AutoMapper;
using Grpc.Core;
using ShoppingApp.Services.Discount.Grpc.Models;
using ShoppingApp.Services.Discount.Grpc.Protos;
using ShoppingApp.Services.Discount.Grpc.Repositories;

namespace ShoppingApp.Services.Discount.Grpc.Services;

public class GetDiscoutService : GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceBase
{
	private readonly IMapper _mapper;
	private readonly IDiscountRepository _repository;
	private readonly ILogger<GetDiscoutService> _logger;

	public GetDiscoutService(
		IMapper mapper,
		IDiscountRepository discountRepository,
		ILogger<GetDiscoutService> logger)
	{
		_mapper = mapper;
		_repository = discountRepository;
		_logger = logger;
	}

	public override async Task<GetDiscountProtocolBufferEntity> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		DiscountModel? discountModel;

		try
		{
			discountModel = await _repository.GetDiscountByProductNameAsync(request.ProductName);
		}
		catch (Exception)
		{
			_logger.LogError("Discount for product name '{ProductName}' was not found.",
				request.ProductName);

			Status status = new(
				StatusCode.Internal,
				$"Discount for product name '{request.ProductName}' was not found.");

			throw new RpcException(status);
		}

		if (discountModel == null)
		{
			this._logger.LogError("Unable to get discount for product '{ProductName}'.",
				request.ProductName);

			Status status = new(
				StatusCode.Internal,
				$"Discount for product name '{request.ProductName}' was not found.");

			throw new RpcException(status);
		}

		_logger.LogInformation(
			"Discount for product '{ProductName}' and amount {Amount} was returned successfully.",
			discountModel.ProductName,
			discountModel.Amount);

		return _mapper.Map<GetDiscountProtocolBufferEntity>(discountModel);
	}
}
