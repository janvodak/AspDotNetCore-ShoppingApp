using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Src.Contracts.Persistence;
using Order.Application.Src.Exceptions;
using Order.Domain.Src.Order.Entities;

namespace Order.Application.Src.Features.Order.Commands.UpdateOrder
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
			this._orderRepository = orderRepository;
			this._mapper = mapper;
			this._logger = logger;
		}

		public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			OrderEntity? order = await this._orderRepository.GetByIdAsync(request.Id)
				?? throw new NotFoundException(nameof(OrderEntity), request.Id);

			this._mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(OrderEntity));

			await this._orderRepository.UpdateAsync(order);

			this._logger.LogInformation("Order '{OrderId}' was successfully updated.", order.Id);

			return Unit.Value;
		}
	}
}
