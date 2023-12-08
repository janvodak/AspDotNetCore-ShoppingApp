using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;

namespace ShoppingApp.Services.Authentication.API.Models.Factories
{
	public interface IAuthenticationUserFactory
	{
		AuthenticationUser Create(RegistrationRequestDataTransferObject registrationRequestDataTransferObject);
	}
}
