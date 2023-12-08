using ShoppingApp.Services.Authentication.API.Models;

namespace ShoppingApp.Services.Authentication.API.Repositories
{
	public interface IUserRepository
	{
		Task<AuthenticationUser?> GetUserByUserNameAsync(string userName);

		Task<AuthenticationUser?> GetUserByEmailAsync(string email);
	}
}
