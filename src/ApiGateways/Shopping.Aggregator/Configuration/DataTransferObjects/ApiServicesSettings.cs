namespace ShoppingApp.ApiGateway.ShoppingAggregator.Configuration.DataTransferObjects
{
	public class ApiServicesSettings
	{
		public const string SECTION_NAME = "ApiSettings";

		public string BasketApiUrl { get; set; } = null!;

		public string ProductApiUrl { get; set; } = null!;

		public string OrderApiUrl { get; set; } = null!;
	}
}
