using Basket.API.Src.Entities;

namespace Basket.API.Src.Repositories
{
	public interface IBasketRepository
	{
		Task<BasketEntity?> GetBasket(string userName);

		Task<BasketEntity?> UpdateBasket(BasketEntity basket);

		Task DeleteBasket(string userName);
	}
}
