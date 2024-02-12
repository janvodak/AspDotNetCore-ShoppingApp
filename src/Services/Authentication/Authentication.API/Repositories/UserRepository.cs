using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Authentication.API.Data;
using ShoppingApp.Services.Authentication.API.Models;

namespace ShoppingApp.Services.Authentication.API.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AuthenticationDbContext _authenticationDbContext;

		public UserRepository(AuthenticationDbContext authenticationDbContext)
		{
			_authenticationDbContext = authenticationDbContext;
		}

		public async Task<AuthenticationUser?> GetUserByEmailAsync(string email)
		{
			return await _authenticationDbContext.AuthenticationUsers.FirstAsync(
				u => u.Email.ToLower() == email.ToLower());
		}

		public async Task<AuthenticationUser?> GetUserByUserNameAsync(string userName)
		{
			return await _authenticationDbContext.AuthenticationUsers.FirstAsync(
				u => u.UserName.ToLower() == userName.ToLower());
		}
	}
}
