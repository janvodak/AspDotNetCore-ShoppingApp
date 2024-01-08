using AutoMapper;
using Basket.API.Src.Entities;
using ShoppingApp.Components.EventBus.Messages.Events;

namespace Basket.API.Src.Mapper
{
	public class BasketProfile : Profile
	{
		public BasketProfile()
		{
			CreateMap<BasketCheckoutEntity, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
