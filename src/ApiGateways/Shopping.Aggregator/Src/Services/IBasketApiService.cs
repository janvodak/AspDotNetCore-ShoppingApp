using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Services
{
	public interface IBasketApiService
	{
		Task<Basket> GetBasketAsync(string username);
	}
}
