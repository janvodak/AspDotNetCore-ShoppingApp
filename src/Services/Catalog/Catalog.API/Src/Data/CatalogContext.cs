using Catalog.API.Src.Entities;
using MongoDB.Driver;

namespace Catalog.API.Src.Data
{
	public class CatalogContext : ICatalogContext
	{
		private const string _connectionStringConfigurationKey = "DatabaseSettings:ConnectionString";
		private const string _databaseNameConfigurationKey = "DatabaseSettings:DatabaseName";
		private const string _connectionStringKey = "DatabaseSettings:CollectionName";

		private readonly IMongoClient _mongoClient;
		private readonly IMongoDatabase _mongoDatabase;

		public IMongoCollection<Product> Products { get; }

		public CatalogContext(IConfiguration configuration)
		{
			this._mongoClient = CreateClient(configuration);
			this._mongoDatabase = this.ConnectToMongoDatabase(configuration);

			this.Products = GetProductCollection(configuration);

			this.SeedDataIfProductsAreEmpty();
		}

		private MongoClient CreateClient(IConfiguration configuration)
		{
			string? connectionStringValue = configuration.GetValue<string>(_connectionStringConfigurationKey)
								   ?? throw new ArgumentNullException(nameof(this._mongoClient));

			return new MongoClient(connectionStringValue);
		}

		private IMongoDatabase ConnectToMongoDatabase(IConfiguration configuration)
		{
			string? databaseName = configuration.GetValue<string>(_databaseNameConfigurationKey)
						  ?? throw new ArgumentNullException(nameof(this._mongoDatabase));

			return this._mongoClient.GetDatabase(databaseName);
		}

		private IMongoCollection<Product> GetProductCollection(IConfiguration configuration)
		{
			string? collectionName = configuration.GetValue<string>(_connectionStringKey)
							?? throw new ArgumentNullException(nameof(this.Products));

			return this._mongoDatabase.GetCollection<Product>(collectionName);
		}

		private void SeedDataIfProductsAreEmpty()
		{
			if (this.Products.Find(p => true).Any() == false)
			{
				CatalogContextSeed.SeedData(this.Products);
			}
		}
	}
}
