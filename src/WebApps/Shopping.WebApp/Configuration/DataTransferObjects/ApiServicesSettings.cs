namespace Shopping.WebApp.Configuration.DataTransferObjects
{
	public class ApiServicesSettings
	{
		public const string SECTION_NAME = "ApiSettings";

		public string OcelotApiGatewayUrl { get; set; } = null!;
	}
}
