namespace Order.Rest.Src.Consumers
{
	public class EventBusSettings
	{
		public const string NAME_OF_SECTION = "EventBusSettings";

		public string HostAddress { get; set; } = null!;

		public string Username { get; set; } = null!;

		public string Password { get; set; } = null!;
	}
}
