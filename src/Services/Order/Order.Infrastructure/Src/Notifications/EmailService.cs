using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Application.Src.Contracts.Notifications;
using Order.Application.Src.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Order.Infrastructure.Src.Notifications
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _emailSettings;
		private readonly ILogger<EmailService> _logger;

		public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
		{
			this._emailSettings = emailSettings.Value;
			this._logger = logger;
		}

		public async Task<bool> SendEmail(Email email)
		{
			SendGridClient client = new(this._emailSettings.ApiKey);

			string subject = email.Subject;
			EmailAddress to = new(email.To);
			string body = email.Body;

			EmailAddress from = new()
			{
				Email = this._emailSettings.FromAddress,
				Name = this._emailSettings.FromName
			};

			SendGridMessage sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, body, body);

			var response = await client.SendEmailAsync(sendGridMessage);

			if (response.StatusCode is System.Net.HttpStatusCode.Accepted
				|| response.StatusCode is System.Net.HttpStatusCode.OK)
			{
				this._logger.LogInformation("Email '{Email}' was successfully sent.", email.ToString());
				return true;
			}

			this._logger.LogError("Unable to send email '{Email}' due to error.", email.ToString());
			return false;
		}
	}
}

