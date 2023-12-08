using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;

namespace ShoppingApp.Services.Authentication.API.Services
{
	public interface IRegistrationService
	{
		Task<UserDataTransferObject> Execute(RegistrationRequestDataTransferObject registrationRequestDataTransferObject);
	}
}
