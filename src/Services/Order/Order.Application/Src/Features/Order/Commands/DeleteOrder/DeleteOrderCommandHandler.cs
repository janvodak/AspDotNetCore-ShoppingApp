using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Src.Contracts.Persistence;
using Order.Application.Src.Exceptions;
using Order.Domain.Src.Order.Entities;

namespace Order.Application.Src.Features.Order.Commands.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<DeleteOrderCommandHandler> _logger;

		public DeleteOrderCommandHandler(
			IOrderRepository orderRepository,
			ILogger<DeleteOrderCommandHandler> logger)
		{
			this._orderRepository = orderRepository;
			this._logger = logger;
		}

		public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			OrderEntity order = await this._orderRepository.GetByIdAsync(request.Id);

			if (order == null)
			{
				throw new NotFoundException(nameof(OrderEntity), request.Id);
			}

			await this._orderRepository.DeleteAsync(order);

			this._logger.LogInformation($"Order '{order.Id}' was successfully deleted.");

			return Unit.Value;
		}
	}
}
