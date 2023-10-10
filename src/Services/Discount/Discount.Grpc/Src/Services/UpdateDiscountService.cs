using AutoMapper;
using Discount.Grpc.Src.Entities;
using Discount.Grpc.Src.Protos;
using Discount.Grpc.Src.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Src.Services;

public class UpdateDiscoutService : UpdateDiscountProtocolBufferService.UpdateDiscountProtocolBufferServiceBase
{
	private readonly IMapper _mapper;
	private readonly IDiscountRepository _repository;

	public UpdateDiscoutService(IMapper mapper, IDiscountRepository discountRepository)
	{
		this._mapper = mapper;
		this._repository = discountRepository;
	}

	public override async Task<UpdateDiscountProtocolBufferEntity> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
	{
		DiscountEntity discountEntity = this._mapper.Map<DiscountEntity>(request.Discount);

		bool isUpdated = await this._repository.UpdateDiscount(discountEntity);


		if (isUpdated == false)
		{
			string errorResponseMessage = $"Unable to update discount for product '{discountEntity.ProductName}' with amount {discountEntity.Amount}.";
			throw new RpcException(new Status(StatusCode.Internal, errorResponseMessage));
		}

		return this._mapper.Map<UpdateDiscountProtocolBufferEntity>(discountEntity);
	}
}
