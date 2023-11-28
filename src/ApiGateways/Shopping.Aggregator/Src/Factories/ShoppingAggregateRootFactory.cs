using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Factories
{
	public class ShoppingAggregateRootFactory
	{
		private readonly BasketFactory _basketFactory;
		private readonly OrderFactory _orderFactory;

		public ShoppingAggregateRootFactory(
			BasketFactory productFactory,
			OrderFactory orderFactory)
		{
			_basketFactory = productFactory;
			_orderFactory = orderFactory;
		}

		public async Task<ShoppingAggregateRoot> Create(string userName)
		{
			Basket? basket = null;

			try
			{
				basket = await _basketFactory.Create(userName);
			}
			catch (Exception)
			{

			}

			IEnumerable<Order> orders = await _orderFactory.Create(userName);

			return new ShoppingAggregateRoot(
				userName,
				orders,
				basket);
		}
	}
}
