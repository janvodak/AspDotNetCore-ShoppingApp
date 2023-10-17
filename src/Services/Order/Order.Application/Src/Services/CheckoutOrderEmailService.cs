using Microsoft.Extensions.Logging;
using Order.Application.Src.Contracts.Infrastructure;
using Order.Application.Src.Models;
using Order.Domain.Src.Order.Entities;

namespace Order.Application.Src.Services
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
			this._logger = logger;
		}

		public async Task SendMail(OrderEntity order)
		{
			var email = new Email(to:"ezozkme@gmail.com", subject:"Order was created", body:$"Order was created.");

			try
			{
				await this.emailService.SendEmail(email);
			}
			catch (Exception exception)
			{
				this._logger.LogError($"Unable to send order '{order.Id}' e-mail due to an error with the email service: '{exception.Message}'");
			}
		}
	}
}
