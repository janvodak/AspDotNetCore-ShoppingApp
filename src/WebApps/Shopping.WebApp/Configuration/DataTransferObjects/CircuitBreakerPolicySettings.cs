namespace Shopping.WebApp.Configuration.DataTransferObjects
{
	public class CircuitBreakerPolicySettings
	{
		public const string SECTION_NAME = "CircuitBreakerPolicy";

		public int MaxFailures { get; set; }

		public int DurationOfBreak { get; set; }
	}
}
