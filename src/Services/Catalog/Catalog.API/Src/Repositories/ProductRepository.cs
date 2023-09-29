using Catalog.API.Src.Data;
using Catalog.API.Src.Entities;
using MongoDB.Driver;

namespace Catalog.API.Src.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IMongoCollection<Product> _productCollection;

		public ProductRepository(ICatalogContext catalogContext)
		{
			this._productCollection = catalogContext.Products;
		}

		public async Task CreateProduct(Product product)
		{
			await this._productCollection.InsertOneAsync(product);
		}

		public async Task<bool> DeleteProduct(string id)
		{
			FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

			DeleteResult result = await this._productCollection.DeleteOneAsync(filter);

			return result.IsAcknowledged == true
				&& result.DeletedCount > 0;
		}

		public async Task<Product> GetProductById(string id)
		{
			return await this._productCollection
				.Find(p => p.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			return await this._productCollection.Find(p => true).ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
		{
			FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);

			return await this._productCollection
				.Find(filter)
				.ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductsByName(string name)
		{
			FilterDefinition<Product> definition = Builders<Product>.Filter.Eq(p => p.Name, name);

			return await this._productCollection.Find(definition)
				.ToListAsync();
		}

		public async Task<bool> UpdateProduct(Product product)
		{
			var result = await this._productCollection
				.ReplaceOneAsync(
					filter: g => g.Id == product.Id,
					replacement: product
				);

			return result.IsAcknowledged == true
				&& result.ModifiedCount > 0;
		}
	}
}

