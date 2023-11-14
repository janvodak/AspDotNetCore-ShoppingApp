using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services
{
	public interface IBasketApiService
	{
		Task<Basket> GetBasket(string username);

		Task<Basket> UpdateBasket(Basket basket);

		Task CheckoutBasket(CheckoutBasket basket);
	}
}
