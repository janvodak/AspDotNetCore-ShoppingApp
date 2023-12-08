using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;

namespace ShoppingApp.Services.Authentication.API.Models.Factories
{
	public class AuthenticationUserFactory : IAuthenticationUserFactory
	{
		public AuthenticationUser Create(RegistrationRequestDataTransferObject registrationRequestDataTransferObject)
		{
			return new AuthenticationUser(
				registrationRequestDataTransferObject.FirstName,
				registrationRequestDataTransferObject.LastName)
			{
				UserName = registrationRequestDataTransferObject.Email,
				Email = registrationRequestDataTransferObject.Email,
				NormalizedEmail = registrationRequestDataTransferObject.Email.ToUpper(),
				PhoneNumber = registrationRequestDataTransferObject.PhoneNumber
			};
		}
	}
}