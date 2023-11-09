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
			this._basketFactory = productFactory;
			this._orderFactory = orderFactory;
		}

		public async Task<ShoppingAggregateRoot> Create(string userName)
		{
			Basket basket = await this._basketFactory.Create(userName);
			IEnumerable<Order> orders = await this._orderFactory.Create(userName);

			return new ShoppingAggregateRoot(
				userName,
				basket,
				orders);
		}
	}
}
