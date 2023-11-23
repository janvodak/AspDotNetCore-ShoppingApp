using AutoMapper;
using ShoppingApp.Services.Discount.Grpc.Models;
using ShoppingApp.Services.Discount.Grpc.Protos;

namespace ShoppingApp.Services.Discount.Grpc.Mappers
{
	public class DiscountProfile : Profile
	{
		public DiscountProfile()
		{
			CreateMap<DiscountModel, CreateDiscountProtocolBufferEntity>().ReverseMap();
			CreateMap<DiscountModel, GetDiscountProtocolBufferEntity>().ReverseMap();
			CreateMap<DiscountModel, UpdateDiscountProtocolBufferEntity>().ReverseMap();
		}
	}
}
