namespace ShoppingApp.Services.Order.API.Infrastructure.Notifications
{
	public class EmailSettings
	{
		public const string SECTION_NAME = "EmailSettings";

		public string ApiKey { get; set; } = null!;

		public string FromAddress { get; set; } = null!;

		public string FromName { get; set; } = null!;
	}
}
