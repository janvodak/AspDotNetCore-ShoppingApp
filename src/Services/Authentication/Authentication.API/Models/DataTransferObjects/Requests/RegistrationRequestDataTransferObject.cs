namespace ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests
{
	public class RegistrationRequestDataTransferObject
	{
		public RegistrationRequestDataTransferObject(
			string email,
			string firstName,
			string lastName,
			string phoneNumber,
			string password)
		{
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phoneNumber;
			Password = password;
		}

		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
