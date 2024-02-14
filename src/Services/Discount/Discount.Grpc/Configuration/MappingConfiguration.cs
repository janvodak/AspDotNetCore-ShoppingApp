using AutoMapper;
using ShoppingApp.Services.Discount.Grpc.Models;
using ShoppingApp.Services.Discount.Grpc.Protos;

namespace ShoppingApp.Services.Discount.Grpc.Configuration
{
	public class MappingConfiguration
	{
		public static MapperConfiguration RegisterMaps()
		{
			MapperConfiguration mappingConfig = new(config =>
			{
				config.CreateMap<DiscountModel, CreateDiscountProtocolBufferEntity>();
				config.CreateMap<DiscountModel, GetDiscountProtocolBufferEntity>();
				config.CreateMap<DiscountModel, UpdateDiscountProtocolBufferEntity>();
			});

			return mappingConfig;
		}
	}
}
