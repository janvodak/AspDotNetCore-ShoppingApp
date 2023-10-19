using Order.Application.Src.Models;

namespace Order.Application.Src.Contracts.Notifications
{
	public interface IEmailService
	{
		Task<bool> SendEmail(Email email);
	}
}

