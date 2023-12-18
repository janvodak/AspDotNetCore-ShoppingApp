using AutoMapper;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.CheckoutOrder;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.DeleteOrder;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.UpdateOrder;
using ShoppingApp.Services.Order.API.Application.Features.Order.Queries.GetOrdersList;
using ShoppingApp.Services.Order.API.Domain.Order;

namespace ShoppingApp.Services.Order.API.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<OrderEntity, OrderDataTransferObject>().ReverseMap();
			CreateMap<OrderEntity, CheckoutOrderCommand>().ReverseMap();
			CreateMap<OrderEntity, UpdateOrderCommand>().ReverseMap();
			CreateMap<OrderEntity, DeleteOrderCommand>().ReverseMap();
		}
	}
}
