using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Services.Authentication.API.Repositories
{
	public class RoleManagerRepository : IRoleManagerRepository
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleManagerRepository(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<IdentityResult> CreateRoleAsync(string roleName)
		{
			return await _roleManager.CreateAsync(new IdentityRole(roleName));
		}

		public async Task<bool> IsRoleExistAsync(string roleName)
		{
			return await _roleManager.RoleExistsAsync(roleName);
		}
	}
}
