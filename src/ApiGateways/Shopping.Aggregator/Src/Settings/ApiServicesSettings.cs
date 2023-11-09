namespace Shopping.Aggregator.Src.Settings
{
	public class ApiServicesSettings
	{
		public const string NAME_OF_SECTION = "ApiSettings";

		public string ProductApiUrl { get; set; } = null!;

		public string BasketApiUrl { get; set; } = null!;

		public string OrderApiUrl { get; set; } = null!;
	}
}
