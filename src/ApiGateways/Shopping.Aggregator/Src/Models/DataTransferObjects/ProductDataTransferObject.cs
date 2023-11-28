namespace Shopping.Aggregator.Src.Models.DataTransferObjects
{
	public class ProductDataTransferObject
	{
		public ProductDataTransferObject(
			string id,
			string name,
			string category,
			string summary,
			string description,
			string imageFile,
			decimal price)
		{
			Id = id;
			Name = name;
			Category = category;
			Summary = summary;
			Description = description;
			ImageFile = imageFile;
			Price = price;
		}

		public string Id { get; set; }

		public string Name { get; set; }

		public string Category { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }

		public string ImageFile { get; set; }

		public decimal Price { get; set; }
	}
}
