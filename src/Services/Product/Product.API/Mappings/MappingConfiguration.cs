using AutoMapper;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;

namespace ShoppingApp.Services.Product.API.Mappings
{
	public class MappingConfiguration
	{
		public static MapperConfiguration RegisterMaps()
		{
			MapperConfiguration mappingConfig = new(config =>
			{
				config.CreateMap<ProductDataTransferObject, ProductModel>().ReverseMap();
			});

			return mappingConfig;
		}
	}
}
