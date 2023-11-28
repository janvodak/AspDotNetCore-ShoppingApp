namespace Shopping.Aggregator.Src.Models.DataTransferObjects
{
	public class BasketProductDataTransferObject
	{
		public BasketProductDataTransferObject(
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
			Id = id;
			Name = name;
			Quantity = quantity;
			Price = price;
			Color = color;
			Category = category;
			Summary = summary;
			Description = description;
			ImageFile = imageFile;
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
