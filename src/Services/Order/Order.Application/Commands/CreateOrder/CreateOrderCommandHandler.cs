using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;

namespace ShoppingApp.Services.Order.API.Application.Commands.CheckoutOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderFactory _orderFactory;
		private readonly ILogger<CreateOrderCommandHandler> _logger;

		public CreateOrderCommandHandler(
			IOrderRepository orderRepository,
			IOrderFactory orderFactory,
			ILogger<CreateOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_orderFactory = orderFactory;
			_logger = logger;
		}

		public async Task<bool> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
		{
			// Add Integration events if needed

			// DDD patterns comment: Add child entities and value-objects through the Aggregate-Root
			// methods and constructor so validations, invariants and business logic
			// make sure that consistency is preserved across the whole aggregate
			OrderAggregateRoot order = _orderFactory.Create(
				command.UserName,
				command.FirstName,
				command.LastName,
				command.EmailAddress,
				command.AddressLine,
				command.Country,
				command.State,
				command.ZipCode,
				command.TotalPrice,
				command.PaymentMethod,
				command.CardName,
				command.CardNumber,
				command.Expiration,
				command.CardVerificationValue);

			_logger.LogInformation("----- Creating Order - Order: {@Order}", order);

			await _orderRepository.AddAsync(order);

			return await _orderRepository.UnitOfWork
				.SaveEntitiesAsync(cancellationToken);
		}
	}
}
