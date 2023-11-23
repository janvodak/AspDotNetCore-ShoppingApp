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

	public UpdateDiscoutService(IMapper mapper, IDiscountRepository discountRepository)
	{
		_mapper = mapper;
		_repository = discountRepository;
	}

	public override async Task<UpdateDiscountProtocolBufferEntity> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
	{
		DiscountModel discountEntity = _mapper.Map<DiscountModel>(request.Discount);

		bool isUpdated = await _repository.UpdateDiscount(discountEntity);


		if (isUpdated == false)
		{
			string errorResponseMessage = $"Unable to update discount for product '{discountEntity.ProductName}' with amount {discountEntity.Amount}.";
			throw new RpcException(new Status(StatusCode.Internal, errorResponseMessage));
		}

		return _mapper.Map<UpdateDiscountProtocolBufferEntity>(discountEntity);
	}
}
