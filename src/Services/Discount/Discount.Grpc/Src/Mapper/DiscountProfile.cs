using AutoMapper;
using Discount.Grpc.Src.Entities;
using Discount.Grpc.Src.Protos;

namespace Discount.Grpc.Src.Mapper
{
	public class DiscountProfile : Profile
	{
		public DiscountProfile()
		{
			CreateMap<DiscountEntity, CreateDiscountProtocolBufferEntity>().ReverseMap();
			CreateMap<DiscountEntity, GetDiscountProtocolBufferEntity>().ReverseMap();
			CreateMap<DiscountEntity, UpdateDiscountProtocolBufferEntity>().ReverseMap();
		}
	}
}
