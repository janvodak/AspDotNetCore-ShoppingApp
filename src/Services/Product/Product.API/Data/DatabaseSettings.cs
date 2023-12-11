namespace ShoppingApp.Services.Product.API.Data
{
	public class DatabaseSettings
	{
		public const string SECTION_NAME = "DatabaseSettings";

		public string ConnectionString { get; set; } = null!;

		public string DatabaseName { get; set; } = null!;

		public string CollectionName { get; set; } = null!;
	}
}
