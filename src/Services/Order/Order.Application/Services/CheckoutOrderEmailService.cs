using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Contracts.Notifications;
using ShoppingApp.Services.Order.API.Application.Models;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;

namespace ShoppingApp.Services.Order.API.Application.Services
{
	public class CheckoutOrderEmailService
	{
		private readonly IEmailService emailService;
		private readonly ILogger<CheckoutOrderEmailService> _logger;

		public CheckoutOrderEmailService(
			IEmailService emailService,
			ILogger<CheckoutOrderEmailService> logger)
		{
			this.emailService = emailService;
			_logger = logger;
		}

		public async Task Send(OrderAggregateRoot order)
		{
			var email = new Email(
				to: order.EmailAddress,
				subject: "Order was created",
				body: $"Order was created.");

			try
			{
				await emailService.SendEmail(email);
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
