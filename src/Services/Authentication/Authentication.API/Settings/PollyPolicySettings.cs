namespace ShoppingApp.Services.Authentication.API.Settings
{
	public class PollyPolicySettings
	{
		public const string NAME_OF_SECTION = "PollyPolicies";

		public int MaxRetryAttempts { get; set; }

		public int SecondsBetweenRetries { get; set; }
	}
}
