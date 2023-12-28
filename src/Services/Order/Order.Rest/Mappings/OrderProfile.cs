using AutoMapper;
using EventBus.Messages.Src.Events;
using ShoppingApp.Services.Order.API.Application.Commands.CheckoutOrder;

namespace ShoppingApp.Services.Order.API.Rest.Mappings
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<CreateOrderCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
