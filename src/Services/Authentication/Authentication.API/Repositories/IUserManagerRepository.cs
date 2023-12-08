using Microsoft.AspNetCore.Identity;
using ShoppingApp.Services.Authentication.API.Models;

namespace ShoppingApp.Services.Authentication.API.Repositories
{
	public interface IUserManagerRepository
	{
		Task<IdentityResult> AddToRoleAsync(AuthenticationUser user, string roleName);

		Task<bool> CheckPasswordAsync(AuthenticationUser user, string password);

		Task<IList<string>> GetRolesAsync(AuthenticationUser user);

		Task<IdentityResult> CreateAsync(AuthenticationUser user, string password);
	}
}

