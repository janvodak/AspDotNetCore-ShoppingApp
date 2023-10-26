using AutoMapper;
using Basket.API.Src.Entities;
using EventBus.Messages.Src.Events;

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
