using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingApp.Services.Product.API.Models;

namespace ShoppingApp.Services.Product.API.Data
{
	public class ProductDbContext : IProductDbContext
	{
		private readonly IMongoCollection<ProductModel> _productMongoCollection;

		public ProductDbContext(IOptions<DatabaseSettings> databaseSettings)
		{
			MongoClient mongoClient = new(connectionString: databaseSettings.Value.ConnectionString);
			IMongoDatabase mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

			_productMongoCollection = mongoDatabase.GetCollection<ProductModel>(databaseSettings.Value.CollectionName);

			SeedDataIfProductsAreEmpty();
		}

		public IMongoCollection<ProductModel> GetProductMongoCollection()
		{
			return _productMongoCollection;
		}

		private void SeedDataIfProductsAreEmpty()
		{
			if (GetProductMongoCollection().Find(p => true).Any() == false)
			{
				ProductDbContextSeed.SeedData(GetProductMongoCollection());
			}
		}
	}
}
