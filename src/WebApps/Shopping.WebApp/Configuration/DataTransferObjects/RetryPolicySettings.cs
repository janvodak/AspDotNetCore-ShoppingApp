namespace Shopping.WebApp.Configuration.DataTransferObjects
{
	public class RetryPolicySettings
	{
		public const string SECTION_NAME = "RetryPolicy";

		public int MaxRetryAttempts { get; set; }

		public int SecondsBetweenRetries { get; set; }

		public int JittererLimit { get; set; }
	}
}
