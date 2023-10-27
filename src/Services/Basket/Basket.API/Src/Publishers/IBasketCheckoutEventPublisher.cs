using Basket.API.Src.Entities;

namespace Basket.API.Src.Publishers
{
	public interface IBasketCheckoutEventPublisher
	{
		Task Publish(BasketCheckoutEntity checkoutBasket);
	}
}
