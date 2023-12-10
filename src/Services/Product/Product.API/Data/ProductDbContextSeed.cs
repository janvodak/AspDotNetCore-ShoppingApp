using MongoDB.Driver;
using ShoppingApp.Services.Product.API.Models;

namespace ShoppingApp.Services.Product.API.Data
{
	public class ProductDbContextSeed
	{
		public static void SeedData(IMongoCollection<ProductModel> productCollection)
		{
			IEnumerable<ProductModel> products = GetConfiguredProducts();
			productCollection.InsertManyAsync(products);
		}

		private static IEnumerable<ProductModel> GetConfiguredProducts()
		{
			return new List<ProductModel>()
			{
				new ProductModel(
					"602d2149e773f2a3990b47f5",
					"IPhone X",
					"Smart Phone",
					"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
					"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
					"product-1.png",
					950.00M
				),
				new ProductModel(
					"602d2149e773f2a3990b47f6",
					"Samsung 10",
					"Smart Phone",
					"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
					"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
					"product-2.png",
					840.00M
				),
				new ProductModel(
					"602d2149e773f2a3990b47f7",
					"Huawei Plus",
					"White Appliances",
					"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
					"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
					"product-3.png",
					650.00M
				),
				new ProductModel(
					"602d2149e773f2a3990b47f8",
					"Xiaomi Mi 9",
					"White Appliances",
					"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
					"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
					"product-4.png",
					470.00M
				),
				new ProductModel(
					"602d2149e773f2a3990b47f9",
					"HTC U11+ Plus",
					"Smart Phone",
					"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
					"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
					"product-5.png",
					380.00M
				),
				new ProductModel(
					"602d2149e773f2a3990b47fa",
					"LG G7 ThinQ",
					"Home Kitchen",
					"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
					"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
					"product-6.png",
					240.00M
				)
			};
		}
	}
}
