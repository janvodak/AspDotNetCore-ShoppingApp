using ShoppingApp.Services.Order.API.Application.Models;

namespace ShoppingApp.Services.Order.API.Application.Contracts.Notifications
{
	public interface IEmailService
	{
		Task<bool> SendEmail(Email email);
	}
}
