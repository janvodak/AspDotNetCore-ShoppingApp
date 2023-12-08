using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Responses;

namespace ShoppingApp.Services.Authentication.API.Services
{
	public interface ILoginService
	{
		Task<LoginResponseDataTransferObject> Execute(LoginRequestDataTransferObject loginRequestDataTransferObject);
	}
}
