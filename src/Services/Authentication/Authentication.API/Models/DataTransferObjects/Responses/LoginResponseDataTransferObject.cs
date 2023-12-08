namespace ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Responses
{
	public class LoginResponseDataTransferObject
	{
		public LoginResponseDataTransferObject()
		{
			User = null;
			Token = "";
		}

		public LoginResponseDataTransferObject(
			UserDataTransferObject user,
			string token)
		{
			User = user;
			Token = token;
		}

		public UserDataTransferObject? User { get; set; }
		public string Token { get; set; }
	}
}
