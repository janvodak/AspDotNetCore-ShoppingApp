﻿namespace Shopping.WebApp.Models
{
	public class Product
	{
		public Product(
			string id,
			string name,
			string category,
			string summary,
			string description,
			string imageFile,
			decimal price)
		{
			this.Id = id;
			this.Name = name;
			this.Category = category;
			this.Summary = summary;
			this.Description = description;
			this.ImageFile = imageFile;
			this.Price = price;
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
