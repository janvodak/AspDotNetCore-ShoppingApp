using Microsoft.AspNetCore.Identity;
using ShoppingApp.Services.Authentication.API.Models;

namespace ShoppingApp.Services.Authentication.API.Repositories
{
	public class UserManagerRepository : IUserManagerRepository
	{
		private readonly UserManager<AuthenticationUser> _userManager;

		public UserManagerRepository(UserManager<AuthenticationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IdentityResult> AddToRoleAsync(AuthenticationUser user, string roleName)
		{
			return await _userManager.AddToRoleAsync(user, roleName);
		}

		public async Task<bool> CheckPasswordAsync(AuthenticationUser user, string password)
		{
			return await _userManager.CheckPasswordAsync(user, password);
		}

		public async Task<IList<string>> GetRolesAsync(AuthenticationUser user)
		{
			return await _userManager.GetRolesAsync(user);
		}

		public async Task<IdentityResult> CreateAsync(AuthenticationUser user, string password)
		{
			return await _userManager.CreateAsync(user, password);
		}
	}
}
