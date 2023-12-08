namespace ShoppingApp.Services.Authentication.API.Models
{
	public class JwtOptions
	{
		public const string SECTION_NAME = "JwtOptions";

		public string Issuer { get; set; } = null!;

		public string Audience { get; set; } = null!;

		public string Secret { get; set; } = null!;

		public int Expiration { get; set; }
	}
}
