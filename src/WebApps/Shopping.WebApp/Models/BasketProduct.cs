namespace Shopping.WebApp.Models
{
	public class BasketProduct
	{
		public BasketProduct(
			string id,
			string name,
			int quantity,
			decimal price,
			string color)
		{
			this.Id = id;
			this.Name = name;
			this.Quantity = quantity;
			this.Price = price;
			this.Color = color;
		}

		public string Id { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }

		public string Color { get; set; }
	}
}
