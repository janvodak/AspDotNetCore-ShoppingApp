using ShoppingApp.Services.Authentication.API.Models;
using ShoppingApp.Services.Authentication.API.Repositories;

namespace ShoppingApp.Services.Authentication.API.Services
{
	public class AssignRoleService : IAssignRoleService
	{
		private readonly IRoleManagerRepository _roleManagerRepository;
		private readonly IUserManagerRepository _userManagerRepository;
		private readonly IUserRepository _userRepository;
		private readonly ILogger<AssignRoleService> _logger;

		public AssignRoleService(
			IRoleManagerRepository roleManagerRepository,
			IUserManagerRepository userManagerRepository,
			IUserRepository userRepository,
			ILogger<AssignRoleService> logger)
		{
			_roleManagerRepository = roleManagerRepository;
			_userManagerRepository = userManagerRepository;
			_userRepository = userRepository;
			_logger = logger;
		}

		public async Task Execute(string email, string roleName)
		{
			AuthenticationUser? user;

			try
			{
				user = await _userRepository.GetUserByEmailAsync(email);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Unable to get user during role assignment process.");

				throw new ApplicationException("Error Encountered.", ex);
			}

			if (user == null)
			{
				_logger.LogError("Unable to get user during role assignment process.");

				throw new ApplicationException("Error Encountered.");
			}

			if (_roleManagerRepository.IsRoleExistAsync(roleName).GetAwaiter().GetResult() == false)
			{
				await _roleManagerRepository.CreateRoleAsync(roleName);
			}

			await _userManagerRepository.AddToRoleAsync(user, roleName);
		}
	}
}
