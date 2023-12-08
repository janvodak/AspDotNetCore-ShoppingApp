namespace ShoppingApp.Services.Authentication.API.Models.DataTransferObjects
{
	public class UserDataTransferObject
	{
		public UserDataTransferObject(
			string iD,
			string firstName,
			string lastName,
			string? email = null,
			string? phoneNumber = null)
		{
			ID = iD;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
		}

		public string ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
