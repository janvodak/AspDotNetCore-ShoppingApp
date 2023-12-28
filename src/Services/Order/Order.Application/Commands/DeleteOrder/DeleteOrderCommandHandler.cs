using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Exceptions;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;

namespace ShoppingApp.Services.Order.API.Application.Commands.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<DeleteOrderCommandHandler> _logger;

		public DeleteOrderCommandHandler(
			IOrderRepository orderRepository,
			ILogger<DeleteOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
		{
			OrderAggregateRoot? order = await _orderRepository.GetByIdAsync(command.Id)
				?? throw new NotFoundException(nameof(OrderAggregateRoot), command.Id);

			_orderRepository.Delete(order);

			_logger.LogInformation("Order '{ID}' was successfully deleted.", order.Id);

			return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		}
	}
}
