using AutoMapper;
using Grpc.Core;
using ShoppingApp.Services.Discount.Grpc.Models;
using ShoppingApp.Services.Discount.Grpc.Protos;
using ShoppingApp.Services.Discount.Grpc.Repositories;

namespace ShoppingApp.Services.Discount.Grpc.Services;

public class UpdateDiscoutService : UpdateDiscountProtocolBufferService.UpdateDiscountProtocolBufferServiceBase
{
	private readonly IMapper _mapper;
	private readonly IDiscountRepository _repository;
	private readonly ILogger<UpdateDiscoutService> _logger;

	public UpdateDiscoutService(
		IMapper mapper,
		IDiscountRepository discountRepository,
		ILogger<UpdateDiscoutService> logger)
	{
		_mapper = mapper;
		_repository = discountRepository;
		_logger = logger;
	}

	public override async Task<UpdateDiscountProtocolBufferEntity> UpdateDiscount(
		UpdateDiscountRequest request,
		ServerCallContext context)
	{
		DiscountModel discountModel = _mapper.Map<DiscountModel>(request.Discount);

		int result;

		try
		{
			result = await _repository.UpdateDiscountAsync(discountModel);
		}
		catch (Exception ex)
		{
			this._logger.LogError(
				ex,
				"Unable to update discount '{Discount}'.",
				discountModel.ToString());

			Status status = new(
				StatusCode.Internal,
				$"Unable to update discount for product '{discountModel.ProductName}' with amount {discountModel.Amount}.");

			throw new RpcException(status);
		}

		if (result == 0)
		{
			_logger.LogError(
				"Unable to update discount '{Discount}'.",
				discountModel.ToString());

			Status status = new(
				StatusCode.Internal,
				$"Unable to update discount for product '{discountModel.ProductName}' with amount {discountModel.Amount}.");

			throw new RpcException(status);
		}

		_logger.LogInformation(
			"Discount for product '{ProductName}' and amount {Amount} was updated successfully.",
			discountModel.ProductName,
			discountModel.Amount);

		return _mapper.Map<UpdateDiscountProtocolBufferEntity>(discountModel);
	}
}
