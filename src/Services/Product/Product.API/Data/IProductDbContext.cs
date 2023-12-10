using MongoDB.Driver;
using ShoppingApp.Services.Product.API.Models;

namespace ShoppingApp.Services.Product.API.Data
{
	public interface IProductDbContext
	{
		IMongoCollection<ProductModel> GetProductMongoCollection();
	}
}
