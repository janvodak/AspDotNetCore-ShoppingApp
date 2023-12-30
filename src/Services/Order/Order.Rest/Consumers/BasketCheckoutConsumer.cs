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

			if (basketCheckoutEvent.CommandId == Guid.Empty)
			{
				_logger.LogWarning(
					"There was an error during BasketCheckoutEvent was consumtion. Empty GUID is not valid for request ID. Order command '{@Order}'.",
					basketCheckoutEvent);

				return;
			}

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

			IdentifiedCommand<CreateOrderCommand, bool> requestCreateOrder = new(command, basketCheckoutEvent.CommandId);

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				requestCreateOrder.GetGenericTypeName(),
				nameof(requestCreateOrder.Command.EmailAddress),
				requestCreateOrder.Command.EmailAddress,
				requestCreateOrder);

			bool commandResult = await _mediator.Send(command);

			if (commandResult == true)
			{
				_logger.LogInformation(
					"BasketCheckoutEvent was consumed successfully. Order command '{@Order}' was successfully processed.",
					command);
			}
			else
			{
				_logger.LogWarning(
					"There was an error during BasketCheckoutEvent was consumtion. Order command '{@Order}'.",
					command);
			}
		}
	}
}
