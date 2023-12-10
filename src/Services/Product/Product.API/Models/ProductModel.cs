using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingApp.Services.Product.API.Models
{
	public class ProductModel
	{
		public ProductModel(
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

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("Name")]
		public string Name { get; set; }

		public string Category { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }

		public string ImageFile { get; set; }

		public decimal Price { get; set; }
	}
}
