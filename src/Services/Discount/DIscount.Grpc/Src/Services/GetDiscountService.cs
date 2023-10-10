using AutoMapper;
using Discount.Grpc.Src.Entities;
using Discount.Grpc.Src.Protos;
using Discount.Grpc.Src.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Src.Services;

public class GetDiscoutService : GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceBase
{
	private readonly IMapper _mapper;
	private readonly IDiscountRepository _repository;

	public GetDiscoutService(IMapper mapper, IDiscountRepository discountRepository)
	{
		this._mapper = mapper;
		this._repository = discountRepository;
	}

	public override async Task<GetDiscountProtocolBufferEntity> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		DiscountEntity? discountEntity = await this._repository.GetDiscount(request.ProductName);

		if (discountEntity == null)
		{
			string errorResponseMessage = $"Discount for product name '{request.ProductName}' was not found.";
			throw new RpcException(new Status(StatusCode.NotFound, errorResponseMessage));
		}

		return this._mapper.Map<GetDiscountProtocolBufferEntity>(discountEntity);
	}
}
