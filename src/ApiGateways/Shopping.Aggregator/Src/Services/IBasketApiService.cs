using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Services
{
	public interface IBasketApiService
	{
		Task<BasketDataTransferObject> GetBasketAsync(string username);
	}
}
