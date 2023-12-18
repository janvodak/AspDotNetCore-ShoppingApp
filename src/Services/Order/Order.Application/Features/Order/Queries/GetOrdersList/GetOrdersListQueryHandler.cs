using AutoMapper;
using MediatR;
using ShoppingApp.Services.Order.API.Application.Contracts.Persistence;
using ShoppingApp.Services.Order.API.Domain.Order;

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
			IEnumerable<OrderEntity> orderList = await _orderRepository.GetOrdersByUserName(request.UserName);

			return _mapper.Map<List<OrderDataTransferObject>>(orderList);
		}
	}
}
