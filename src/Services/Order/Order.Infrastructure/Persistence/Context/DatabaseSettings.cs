namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context
{
	public class DatabaseSettings
	{
		public string ConnectionStringTemplate { get; set; } = null!;
		public string Server { get; set; } = null!;
		public string DBname { get; set; } = null!;
		public string User { get; set; } = null!;
		public string Password { get; set; } = null!;

		public string GetConnectionString()
		{
			return string.Format(
				ConnectionStringTemplate,
				Server,
				DBname,
				User,
				Password);
		}
	}
}
