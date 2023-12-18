using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using ShoppingApp.Services.Order.API.Application.Contracts.Notifications;
using ShoppingApp.Services.Order.API.Application.Models;

namespace ShoppingApp.Services.Order.API.Infrastructure.Notifications
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _emailSettings;
		private readonly ILogger<EmailService> _logger;

		public EmailService(
			IOptions<EmailSettings> emailSettings,
			ILogger<EmailService> logger)
		{
			_emailSettings = emailSettings.Value;
			_logger = logger;
		}

		public async Task<bool> SendEmail(Email email)
		{
			SendGridClient client = new(_emailSettings.ApiKey);

			string subject = email.Subject;
			EmailAddress to = new(email.To);
			string body = email.Body;

			EmailAddress from = new()
			{
				Email = _emailSettings.FromAddress,
				Name = _emailSettings.FromName
			};

			SendGridMessage sendGridMessage = MailHelper.CreateSingleEmail(
				from,
				to,
				subject,
				body,
				body);

			var response = await client.SendEmailAsync(sendGridMessage);

			if (response.StatusCode is System.Net.HttpStatusCode.Accepted
				|| response.StatusCode is System.Net.HttpStatusCode.OK)
			{
				_logger.LogInformation(
					"Email '{Email}' was successfully sent.",
					email.ToString());

				return true;
			}

			_logger.LogError(
				"Unable to send email '{Email}' due to error.",
				email.ToString());

			return false;
		}
	}
}
