namespace ShoppingApp.Services.Discount.Grpc.Data;

public class DatabaseSettings
{
	public string User { get; set; } = null!;

	public string Password { get; set; } = null!;

	public string Host { get; set; } = null!;

	public string Port { get; set; } = null!;

	public string DBname { get; set; } = null!;
}
