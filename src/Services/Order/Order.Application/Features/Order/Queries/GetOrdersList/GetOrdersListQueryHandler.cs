using AutoMapper;
using MediatR;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;

namespace ShoppingApp.Services.Order.API.Application.Features.Order.Queries.GetOrdersList
{
	public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDataTransferObject>>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<List<OrderDataTransferObject>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
		{
			IEnumerable<OrderAggregateRoot> orderList = await _orderRepository.GetOrdersByUserName(request.UserName);

			return _mapper.Map<List<OrderDataTransferObject>>(orderList);
		}
	}
}
