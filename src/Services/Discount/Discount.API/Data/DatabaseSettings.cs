namespace ShoppingApp.Services.Discount.API.Data
{
	public class DatabaseSettings
	{
		public const string SECTION_NAME = "DatabaseSettings";

		public string ConnectionStringTemplate { get; set; } = null!;

		public string User { get; set; } = null!;

		public string Password { get; set; } = null!;

		public string Host { get; set; } = null!;

		public string Port { get; set; } = null!;

		public string DBname { get; set; } = null!;

		public string GetConnectionString()
		{
			return string.Format(
				ConnectionStringTemplate,
				User,
				Password,
				Host,
				Port,
				DBname);
		}
	}
}
