using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Services.Authentication.API.Models
{
	public class AuthenticationUser : IdentityUser
	{
		public AuthenticationUser(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
