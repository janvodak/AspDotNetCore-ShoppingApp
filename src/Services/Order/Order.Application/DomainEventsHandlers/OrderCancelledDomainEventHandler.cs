using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Contracts.Notifications;
using ShoppingApp.Services.Order.API.Application.Models;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.Events;

namespace ShoppingApp.Services.Order.API.Application.DomainEventsHandlers
{
	public partial class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelledDomainEvent>
	{
		private readonly IEmailService _emailService;
		private readonly ILogger<OrderCancelledDomainEventHandler> _logger;

		public OrderCancelledDomainEventHandler(
			IEmailService emailService,
			ILogger<OrderCancelledDomainEventHandler> logger)
		{
			_emailService = emailService;
			_logger = logger;
		}

		public async Task Handle(OrderCancelledDomainEvent domainEvent, CancellationToken cancellationToken)
		{
			OrderAggregateRoot order = domainEvent.Order;

			var email = new Email(
				to: order.BillingAddress.EmailAddress,
				subject: "Order was cancelled",
				body: $"Order with ID {order.Id} was cancelled.");

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
