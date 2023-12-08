namespace ShoppingApp.Services.Authentication.API.Models
{
	public class JwtToken
	{
		public JwtToken(string value)
		{
			Value = value;
		}

		public string Value { get; set; }
	}
}
