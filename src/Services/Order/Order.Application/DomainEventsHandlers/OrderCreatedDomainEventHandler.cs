using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Contracts.Notifications;
using ShoppingApp.Services.Order.API.Application.Models;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.Events;

namespace ShoppingApp.Services.Order.API.Application.DomainEventsHandlers
{
	public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
	{
		private readonly IEmailService _emailService;
		private readonly ILogger<OrderCreatedDomainEventHandler> _logger;

		public OrderCreatedDomainEventHandler(
			IEmailService emailService,
			ILogger<OrderCreatedDomainEventHandler> logger)
		{
			this._emailService = emailService;
			_logger = logger;
		}

		public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
		{
			OrderAggregateRoot order = domainEvent.Order;

			var email = new Email(
				to: order.BillingAddress.EmailAddress,
				subject: "Order was created",
				body: $"Order with ID {order.Id} was created.");

			try
			{
				await _emailService.SendEmail(email);
			}
			catch (Exception exception)
			{
				_logger.LogError(
					"Unable to send order '{OrderId}' e-mail due to an error with the email service: '{PreviousMessage}'",
					order.Id,
					exception.Message);
			}
		}
	}
}
