using AutoMapper;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.CheckoutOrder;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.DeleteOrder;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.UpdateOrder;
using ShoppingApp.Services.Order.API.Application.Features.Order.Queries.GetOrdersList;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;

namespace ShoppingApp.Services.Order.API.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<OrderAggregateRoot, OrderDataTransferObject>().ReverseMap();
			CreateMap<OrderAggregateRoot, CheckoutOrderCommand>().ReverseMap();
			CreateMap<OrderAggregateRoot, UpdateOrderCommand>().ReverseMap();
			CreateMap<OrderAggregateRoot, DeleteOrderCommand>().ReverseMap();
		}
	}
}
