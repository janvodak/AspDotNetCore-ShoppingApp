using Microsoft.AspNetCore.Identity;
using ShoppingApp.Services.Authentication.API.Models;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;
using ShoppingApp.Services.Authentication.API.Models.Factories;
using ShoppingApp.Services.Authentication.API.Repositories;

namespace ShoppingApp.Services.Authentication.API.Services
{
	public class RegisterService : IRegistrationService
	{
		private readonly IUserManagerRepository _userManagerRepository;
		private readonly IUserRepository _userRepository;
		private readonly ILogger<RegisterService> _logger;
		private readonly IAuthenticationUserFactory _authenticationUserFactory;

		public RegisterService(
			IUserManagerRepository userManagerRepository,
			IUserRepository userRepository,
			ILogger<RegisterService> logger,
			IAuthenticationUserFactory authenticationUserFactory)
		{
			_userManagerRepository = userManagerRepository;
			_userRepository = userRepository;
			_logger = logger;
			_authenticationUserFactory = authenticationUserFactory;
		}

		public async Task<UserDataTransferObject> Execute(RegistrationRequestDataTransferObject registrationRequestDataTransferObject)
		{
			AuthenticationUser authenticationUser = _authenticationUserFactory.Create(registrationRequestDataTransferObject);

			IdentityResult result = await _userManagerRepository.CreateAsync(authenticationUser, registrationRequestDataTransferObject.Password);

			if (result.Succeeded == false)
			{
				throw new ApplicationException(result.Errors.FirstOrDefault()?.Description);
			}

			AuthenticationUser? userToReturn;

			try
			{
				userToReturn = await _userRepository.GetUserByUserNameAsync(registrationRequestDataTransferObject.Email);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unable to get user during registration process.");

				throw new ApplicationException("Error Encountered.", ex);
			}

			if (userToReturn == null)
			{
				_logger.LogError("Unable to get user during registration process.");

				throw new ApplicationException("Error Encountered.");
			}

			return new UserDataTransferObject(
				userToReturn.Id,
				userToReturn.FirstName,
				userToReturn.LastName,
				userToReturn.Email,
				userToReturn.PhoneNumber);
		}
	}
}
