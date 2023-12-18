using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Contracts.Persistence;
using ShoppingApp.Services.Order.API.Application.Exceptions;
using ShoppingApp.Services.Order.API.Domain.Order;

namespace ShoppingApp.Services.Order.API.Application.Features.Order.Commands.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
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

		public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			OrderEntity? order = await _orderRepository.GetByIdAsync(request.Id)
				?? throw new NotFoundException(nameof(OrderEntity), request.Id);

			await _orderRepository.DeleteAsync(order);

			_logger.LogInformation($"Order '{order.Id}' was successfully deleted.");

			return Unit.Value;
		}
	}
}
