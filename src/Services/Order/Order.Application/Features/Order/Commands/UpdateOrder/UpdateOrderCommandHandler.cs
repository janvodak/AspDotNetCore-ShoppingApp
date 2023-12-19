using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Exceptions;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;

namespace ShoppingApp.Services.Order.API.Application.Features.Order.Commands.UpdateOrder
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateOrderCommandHandler> _logger;

		public UpdateOrderCommandHandler(
			IOrderRepository orderRepository,
			IMapper mapper,
			ILogger<UpdateOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			OrderAggregateRoot? order = await _orderRepository.GetByIdAsync(request.Id)
				?? throw new NotFoundException(nameof(OrderAggregateRoot), request.Id);

			_mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(OrderAggregateRoot));

			await _orderRepository.UpdateAsync(order);

			_logger.LogInformation("Order '{OrderId}' was successfully updated.", order.Id);

			return Unit.Value;
		}
	}
}
