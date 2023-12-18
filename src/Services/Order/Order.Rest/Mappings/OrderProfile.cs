using AutoMapper;
using EventBus.Messages.Src.Events;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.CheckoutOrder;

namespace ShoppingApp.Services.Order.API.Rest.Mappings
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
