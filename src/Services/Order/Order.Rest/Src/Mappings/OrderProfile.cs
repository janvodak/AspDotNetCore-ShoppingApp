using AutoMapper;
using EventBus.Messages.Src.Events;
using Order.Application.Src.Features.Order.Commands.CheckoutOrder;

namespace Order.Rest.Src.Mappings
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
