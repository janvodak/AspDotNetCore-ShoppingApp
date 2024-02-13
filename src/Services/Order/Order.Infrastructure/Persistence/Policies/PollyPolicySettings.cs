namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Policies
{
	public class PollyPolicySettings
	{
		public const string SECTION_NAME = "PollyPolicies";

		public int MaxRetryAttempts { get; set; }

		public int SecondsBetweenRetries { get; set; }
	}
}
