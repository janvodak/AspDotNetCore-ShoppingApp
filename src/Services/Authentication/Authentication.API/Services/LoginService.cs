using ShoppingApp.Services.Authentication.API.Models;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Responses;
using ShoppingApp.Services.Authentication.API.Repositories;

namespace ShoppingApp.Services.Authentication.API.Services
{
	public class LoginService : ILoginService
	{
		private readonly IUserManagerRepository _userManagerRepository;
		private readonly IUserRepository _userRepository;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		private readonly ILogger<LoginService> _logger;

		public LoginService(
			IUserManagerRepository userManagerRepository,
			IUserRepository userRepository,
			IJwtTokenGenerator jwtTokenGenerator,
			ILogger<LoginService> logger)
		{
			_userManagerRepository = userManagerRepository;
			_userRepository = userRepository;
			_jwtTokenGenerator = jwtTokenGenerator;
			_logger = logger;
		}

		public async Task<LoginResponseDataTransferObject> Execute(LoginRequestDataTransferObject loginRequestDataTransferObject)
		{
			AuthenticationUser? user;

			try
			{
				user = await _userRepository.GetUserByUserNameAsync(loginRequestDataTransferObject.UserName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unable to get user during login process.");

				throw new ApplicationException("Error Encountered.", ex);
			}

			if (user == null)
			{
				_logger.LogError("Unable to get user during login process.");

				return new LoginResponseDataTransferObject();
			}

			bool isValid = await _userManagerRepository.CheckPasswordAsync(user, loginRequestDataTransferObject.Password);

			if (isValid == false)
			{
				_logger.LogError("Problem with user password validation during login process.");

				return new LoginResponseDataTransferObject();
			}

			IList<string> roles = await _userManagerRepository.GetRolesAsync(user);
			JwtToken token = _jwtTokenGenerator.Generate(user, roles);

			UserDataTransferObject userDataTransferObject = new(
				user.Id,
				user.FirstName,
				user.LastName,
				user.Email,
				user.PhoneNumber);

			return new LoginResponseDataTransferObject(
				userDataTransferObject,
				token.Value);
		}
	}
}
