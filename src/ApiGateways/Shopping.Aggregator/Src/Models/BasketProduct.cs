namespace Shopping.Aggregator.Src.Models
{
	public class BasketProduct
	{
		public BasketProduct(
			string id,
			string name,
			int quantity,
			decimal price,
			string color,
			string category,
			string summary,
			string description,
			string imageFile)
		{
			this.Id = id;
			this.Name = name;
			this.Quantity = quantity;
			this.Price = price;
			this.Color = color;
			this.Category = category;
			this.Summary = summary;
			this.Description = description;
			this.ImageFile = imageFile;
		}

		public string Id { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }

		public string Color { get; set; }

		public string Category { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }

		public string ImageFile { get; set; }
	}
}
