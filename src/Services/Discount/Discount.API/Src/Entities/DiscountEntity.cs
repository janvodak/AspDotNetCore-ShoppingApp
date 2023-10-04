namespace Discount.API.Src.Entities
{
	public class DiscountEntity
	{
		public int Id { get; set; }

		public string ProductName { get; set; } = null!;

		public string Description { get; set; } = null!;

		public int Amount { get; set; }
	}
}
