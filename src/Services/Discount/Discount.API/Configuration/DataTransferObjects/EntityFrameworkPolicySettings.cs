namespace ShoppingApp.Services.Discount.API.Configuration.DataTransferObjects
{
	public class EntityFrameworkPolicySettings
	{
		public const string SECTION_NAME = "EntityFrameworkPolicies";

		public int MaxRetryCount { get; set; }

		public int MaxRetryDelay { get; set; }
	}
}
