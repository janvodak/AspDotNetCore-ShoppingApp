using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Exceptions;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;

namespace ShoppingApp.Services.Order.API.Application.Commands.CancelOrder
{
	public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<CancelOrderCommandHandler> _logger;

		public CancelOrderCommandHandler(
			IOrderRepository orderRepository,
			ILogger<CancelOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
		{
			OrderAggregateRoot? order = await _orderRepository.GetByIdAsync(command.Id)
				?? throw new NotFoundException(nameof(OrderAggregateRoot), command.Id);

			order.SetCancelledStatus();

			_logger.LogInformation("Order '{ID}' was successfully cancelled.", order.Id);

			_orderRepository.Update(order);

			return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		}
	}
}
