namespace Basket.API.Src.Entities
{
	public class ProductEntity
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public int Quantity { get; set; }

		public decimal Price { get; set; }

		public string Color { get; set; } = null!;
	}
}
