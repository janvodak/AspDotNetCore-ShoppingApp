using AutoMapper;
using ShoppingApp.Services.Discount.API.Models.DataTransferObjects;

namespace ShoppingApp.Services.Discount.API.Models.Mappings
{
	public class MappingConfiguration
	{
		public static MapperConfiguration RegisterMaps()
		{
			MapperConfiguration mappingConfig = new(config =>
			{
				config.CreateMap<DiscountDataTransferObject, DiscountModel>();
				config.CreateMap<DiscountModel, DiscountDataTransferObject>();
			});

			return mappingConfig;
		}
	}
}
