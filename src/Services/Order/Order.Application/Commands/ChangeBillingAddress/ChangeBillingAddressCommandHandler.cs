using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Exceptions;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;

namespace ShoppingApp.Services.Order.API.Application.Commands.ChangeBillingAddress
{
	public class ChangeBillingAddressCommandHandler : IRequestHandler<ChangeBillingAddressCommand, bool>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IAddressFactory _addressFactory;
		private readonly ILogger<ChangeBillingAddressCommandHandler> _logger;

		public ChangeBillingAddressCommandHandler(
			IOrderRepository orderRepository,
			IAddressFactory addressFactory,
			ILogger<ChangeBillingAddressCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_addressFactory = addressFactory;
			_logger = logger;
		}

		public async Task<bool> Handle(ChangeBillingAddressCommand command, CancellationToken cancellationToken)
		{
			OrderAggregateRoot? order = await _orderRepository.GetByIdAsync(command.Id)
				?? throw new NotFoundException(nameof(OrderAggregateRoot), command.Id);

			// Add Integration events if needed

			// DDD patterns comment: Add child entities and value-objects through the Aggregate-Root
			// methods and constructor so validations, invariants and business logic
			// make sure that consistency is preserved across the whole aggregate
			AddressValueObject address = _addressFactory.Create(
				command.FirstName,
				command.LastName,
				command.EmailAddress,
				command.AddressLine,
				command.Country,
				command.State,
				command.ZipCode);

			order.ChangeBillingAddress(address);

			_logger.LogInformation("----- Updating Billing Address - Order: {@Order}", order);

			_orderRepository.Update(order);

			return await _orderRepository.UnitOfWork
				.SaveEntitiesAsync(cancellationToken);
		}
	}
}
