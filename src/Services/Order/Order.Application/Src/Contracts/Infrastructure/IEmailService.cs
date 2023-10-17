using Order.Application.Src.Models;

namespace Order.Application.Src.Contracts.Infrastructure
{
	public interface IEmailService
	{
		Task<bool> SendEmail(Email email);
	}
}

