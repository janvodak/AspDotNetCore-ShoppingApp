namespace ShoppingApp.Services.Discount.API.Data.Policies
{
	public class PollyPolicySettings
	{
		public const string SECTION_NAME = "PollyPolicies";

		public int MaxRetryAttempts { get; set; }

		public int SecondsBetweenRetries { get; set; }
	}
}
