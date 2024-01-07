using EventBus.Messages.Src.Events;
using MassTransit;
using MediatR;
using ShoppingApp.Services.Order.API.Application.Commands.CreateOrder;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Extensions;

namespace ShoppingApp.Services.Order.API.Rest.Consumers
{
	public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<BasketCheckoutConsumer> _logger;

		public BasketCheckoutConsumer(
			IMediator mediator,
			ILogger<BasketCheckoutConsumer> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
		{
			BasketCheckoutEvent basketCheckoutEvent = context.Message;
			Guid requestId = basketCheckoutEvent.Id;

			_logger.LogInformation(
					"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
					basketCheckoutEvent.GetGenericTypeName(),
					nameof(basketCheckoutEvent.EmailAddress),
					basketCheckoutEvent.EmailAddress,
					basketCheckoutEvent);

			if (requestId == Guid.Empty)
			{
				_logger.LogWarning(
					"Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}",
					basketCheckoutEvent);

				return;
			}

			using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new(
					"IdentifiedCommandId",
					requestId) }))
			{
				CreateOrderCommand command = new(
					basketCheckoutEvent.UserName,
					basketCheckoutEvent.FirstName,
					basketCheckoutEvent.LastName,
					basketCheckoutEvent.EmailAddress,
					basketCheckoutEvent.AddressLine,
					basketCheckoutEvent.Country,
					basketCheckoutEvent.State,
					basketCheckoutEvent.ZipCode,
					basketCheckoutEvent.TotalPrice,
					basketCheckoutEvent.PaymentMethod,
					basketCheckoutEvent.CardName,
					basketCheckoutEvent.CardName,
					basketCheckoutEvent.Expiration,
					basketCheckoutEvent.CardVerificationValue);

				IdentifiedCommand<CreateOrderCommand, bool> requestCreateOrder = new(
					command,
					requestId);

				_logger.LogInformation(
					"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
					requestCreateOrder.GetGenericTypeName(),
					nameof(requestCreateOrder.Id),
					requestCreateOrder.Id,
					requestCreateOrder);

				bool commandResult = await _mediator.Send(command);

				if (commandResult == true)
				{
					_logger.LogInformation(
						"CreateOrderCommand succeeded - RequestId: {RequestId}",
						requestId);

				}
				else
				{
					_logger.LogWarning(
						"CreateOrderCommand failed - RequestId: {RequestId}",
						requestId);

				}
			}
		}
	}
}
