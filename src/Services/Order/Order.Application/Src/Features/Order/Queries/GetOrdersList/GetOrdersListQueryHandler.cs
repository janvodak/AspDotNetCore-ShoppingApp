using AutoMapper;
using MediatR;
using Order.Application.Src.Contracts.Persistence;
using Order.Domain.Src.Order.Entities;

namespace Order.Application.Src.Features.Order.Queries.GetOrdersList
{
	public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDataTransferObject>>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			this._orderRepository = orderRepository;
			this._mapper = mapper;
		}

		public async Task<List<OrderDataTransferObject>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
		{
			IEnumerable<OrderEntity> orderList = await this._orderRepository.GetOrdersByUserName(request.UserName);

			return this._mapper.Map<List<OrderDataTransferObject>>(orderList);
		}
	}
}
