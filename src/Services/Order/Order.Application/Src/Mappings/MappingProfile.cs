using AutoMapper;
using Order.Application.Src.Features.Order.Commands.CheckoutOrder;
using Order.Application.Src.Features.Order.Commands.DeleteOrder;
using Order.Application.Src.Features.Order.Commands.UpdateOrder;
using Order.Application.Src.Features.Order.Queries.GetOrdersList;
using Order.Domain.Src.Order.Entities;

namespace Order.Application.Src.Mappings
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
