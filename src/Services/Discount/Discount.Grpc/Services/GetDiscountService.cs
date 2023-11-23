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

	public GetDiscoutService(IMapper mapper, IDiscountRepository discountRepository)
	{
		_mapper = mapper;
		_repository = discountRepository;
	}

	public override async Task<GetDiscountProtocolBufferEntity> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		DiscountModel? discountEntity = await _repository.GetDiscount(request.ProductName);

		if (discountEntity == null)
		{
			string errorResponseMessage = $"Discount for product name '{request.ProductName}' was not found.";
			throw new RpcException(new Status(StatusCode.NotFound, errorResponseMessage));
		}

		return _mapper.Map<GetDiscountProtocolBufferEntity>(discountEntity);
	}
}
