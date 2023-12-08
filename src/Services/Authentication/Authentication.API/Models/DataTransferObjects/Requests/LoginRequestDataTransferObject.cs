namespace ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests
{
	public class LoginRequestDataTransferObject
	{
		public LoginRequestDataTransferObject(string userName, string password)
		{
			UserName = userName;
			Password = password;
		}

		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
