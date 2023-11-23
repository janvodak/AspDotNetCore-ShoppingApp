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

	public CreateDiscoutService(IMapper mapper, IDiscountRepository discountRepository)
	{
		_mapper = mapper;
		_repository = discountRepository;
	}

	public override async Task<CreateDiscountProtocolBufferEntity> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		DiscountModel discountEntity = _mapper.Map<DiscountModel>(request.Discount);

		bool isCreated = await _repository.CreateDiscount(discountEntity);

		if (isCreated == false)
		{
			string errorResponseMessage = $"Unable to create discount for product '{discountEntity.ProductName}' with amount {discountEntity.Amount}.";
			throw new RpcException(new Status(StatusCode.Internal, errorResponseMessage));
		}

		return _mapper.Map<CreateDiscountProtocolBufferEntity>(discountEntity);
	}
}
