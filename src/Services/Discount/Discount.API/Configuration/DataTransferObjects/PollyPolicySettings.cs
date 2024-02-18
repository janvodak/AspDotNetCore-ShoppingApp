namespace ShoppingApp.Services.Discount.API.Configuration.DataTransferObjects
{
	public class PollyPolicySettings
	{
		public const string SECTION_NAME = "PollyPolicies";

		public int MaxRetryAttempts { get; set; }

		public int SecondsBetweenRetries { get; set; }
	}
}
