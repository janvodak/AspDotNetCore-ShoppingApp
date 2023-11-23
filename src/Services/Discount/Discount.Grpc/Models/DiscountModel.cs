namespace ShoppingApp.Services.Discount.Grpc.Models
{
	public class DiscountModel
	{
		public int Id { get; set; }

		public string ProductName { get; set; } = null!;

		public string Description { get; set; } = null!;

		public int Amount { get; set; }
	}
}
