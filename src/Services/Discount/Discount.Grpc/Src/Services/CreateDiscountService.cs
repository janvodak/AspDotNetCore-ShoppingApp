using AutoMapper;
using Discount.Grpc.Src.Entities;
using Discount.Grpc.Src.Protos;
using Discount.Grpc.Src.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Src.Services;

public class CreateDiscoutService : CreateDiscountProtocolBufferService.CreateDiscountProtocolBufferServiceBase
{
	private readonly IMapper _mapper;
	private readonly IDiscountRepository _repository;

	public CreateDiscoutService(IMapper mapper, IDiscountRepository discountRepository)
	{
		this._mapper = mapper;
		this._repository = discountRepository;
	}

	public override async Task<CreateDiscountProtocolBufferEntity> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		DiscountEntity discountEntity = this._mapper.Map<DiscountEntity>(request.Discount);

		bool isCreated = await this._repository.CreateDiscount(discountEntity);

		if (isCreated == false)
		{
			string errorResponseMessage = $"Unable to create discount for product '{discountEntity.ProductName}' with amount {discountEntity.Amount}.";
			throw new RpcException(new Status(StatusCode.Internal, errorResponseMessage));
		}

		return this._mapper.Map<CreateDiscountProtocolBufferEntity>(discountEntity);
	}
}
