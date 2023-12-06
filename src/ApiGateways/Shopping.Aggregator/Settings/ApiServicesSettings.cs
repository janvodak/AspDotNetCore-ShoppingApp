namespace ShoppingApp.ApiGateway.ShoppingAggregator.Settings
{
	public class ApiServicesSettings
	{
		public const string NAME_OF_SECTION = "ApiSettings";

		public string BasketApiUrl { get; set; } = null!;

		public string ProductApiUrl { get; set; } = null!;

		public string OrderApiUrl { get; set; } = null!;
	}
}
