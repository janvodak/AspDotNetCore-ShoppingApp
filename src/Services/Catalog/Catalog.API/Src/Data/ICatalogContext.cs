
using Catalog.API.Src.Entities;
using MongoDB.Driver;

namespace Catalog.API.Src.Data
{
	public interface ICatalogContext
	{
		IMongoCollection<Product> Products { get; }
	}
}
