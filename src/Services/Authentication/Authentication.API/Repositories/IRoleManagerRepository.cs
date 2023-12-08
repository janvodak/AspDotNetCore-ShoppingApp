using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Services.Authentication.API.Repositories
{
	public interface IRoleManagerRepository
	{
		Task<bool> IsRoleExistAsync(string roleName);

		Task<IdentityResult> CreateRoleAsync(string roleName);
	}
}
