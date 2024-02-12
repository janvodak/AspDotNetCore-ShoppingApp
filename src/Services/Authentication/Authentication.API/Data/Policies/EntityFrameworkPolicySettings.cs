namespace ShoppingApp.Services.Authentication.API.Data.Policies
{
	public class EntityFrameworkPolicySettings
	{
		public const string SECTION_NAME = "EntityFrameworkPolicies";

		public int MaxRetryCount { get; set; }

		public int MaxRetryDelay { get; set; }
	}
}
