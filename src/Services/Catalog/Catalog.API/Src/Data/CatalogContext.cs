using Catalog.API.Src.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Src.Data
{
	public class CatalogContext : ICatalogContext
	{
		private readonly IMongoCollection<Product> _productMongoCollection;

		public CatalogContext(IOptions<DatabaseSettings> databaseSettings)
		{
			MongoClient mongoClient = new(connectionString: databaseSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

			this._productMongoCollection = mongoDatabase.GetCollection<Product>(databaseSettings.Value.CollectionName);

			this.SeedDataIfProductsAreEmpty();
		}

		public IMongoCollection<Product> GetProductMongoCollection()
		{
			return _productMongoCollection;
		}

		private void SeedDataIfProductsAreEmpty()
		{
			if (this.GetProductMongoCollection().Find(p => true).Any() == false)
			{
				CatalogContextSeed.SeedData(this.GetProductMongoCollection());
			}
		}
	}
}
