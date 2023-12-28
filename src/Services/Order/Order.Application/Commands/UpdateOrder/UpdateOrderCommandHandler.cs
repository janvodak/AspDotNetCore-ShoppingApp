using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Exceptions;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;

namespace ShoppingApp.Services.Order.API.Application.Commands.UpdateOrder
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderFactory _orderFactory;
		private readonly ILogger<UpdateOrderCommandHandler> _logger;

		public UpdateOrderCommandHandler(
			IOrderRepository orderRepository,
			IOrderFactory orderFactory,
			ILogger<UpdateOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_orderFactory = orderFactory;
			_logger = logger;
		}

		public async Task<bool> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
		{
			OrderAggregateRoot? order = await _orderRepository.GetByIdAsync(command.Id)
				?? throw new NotFoundException(nameof(OrderAggregateRoot), command.Id);

			// Add Integration events if needed

			// DDD patterns comment: Add child entities and value-objects through the Aggregate-Root
			// methods and constructor so validations, invariants and business logic
			// make sure that consistency is preserved across the whole aggregate

			_logger.LogInformation("----- Updating Order - Order: {@Order}", order);

			_orderRepository.Update(order);

			return await _orderRepository.UnitOfWork
				.SaveEntitiesAsync(cancellationToken);
		}
	}
}
