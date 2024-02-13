namespace Shopping.WebApp.Policies.Configuration
{
	public class RetryPolicySettings
	{
		public const string SECTION_NAME = "RetryPolicy";

		public int MaxRetryAttempts { get; set; }

		public int SecondsBetweenRetries { get; set; }

		public int JittererLimit { get; set; }
	}
}
