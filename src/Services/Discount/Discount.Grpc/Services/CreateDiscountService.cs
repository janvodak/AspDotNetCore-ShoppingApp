using AutoMapper;
using Grpc.Core;
using ShoppingApp.Services.Discount.Grpc.Models;
using ShoppingApp.Services.Discount.Grpc.Protos;
using ShoppingApp.Services.Discount.Grpc.Repositories;

namespace ShoppingApp.Services.Discount.Grpc.Services;

public class CreateDiscoutService : CreateDiscountProtocolBufferService.CreateDiscountProtocolBufferServiceBase
{
	private readonly IMapper _mapper;
	private readonly IDiscountRepository _repository;
	private readonly ILogger<CreateDiscoutService> _logger;

	public CreateDiscoutService(
		IMapper mapper,
		IDiscountRepository discountRepository,
		ILogger<CreateDiscoutService> logger)
	{
		_mapper = mapper;
		_repository = discountRepository;
		_logger = logger;
	}

	public override async Task<CreateDiscountProtocolBufferEntity> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		DiscountModel discountModel = _mapper.Map<DiscountModel>(request.Discount);

		int result = await _repository.CreateDiscountAsync(discountModel);

		if (result == 0)
		{
			_logger.LogError(
				"Unable to create discount: '{Discount}'",
				discountModel.ToString());

			Status status = new(
				StatusCode.Internal,
				$"Unable to create discount for product '{discountModel.ProductName}' with amount {discountModel.Amount}.");

			throw new RpcException(status);
		}

		_logger.LogInformation(
			"Discount for product '{ProductName}' with amount {Amount} was created successfully.",
			discountModel.ProductName,
			discountModel.Amount);

		return _mapper.Map<CreateDiscountProtocolBufferEntity>(discountModel);
	}
}
