using Shopping.Aggregator.Src.Models.DataTransferObjects;
using Shopping.Aggregator.Src.Models.DataTransferObjects.Factories;

namespace Shopping.Aggregator.Src.Models.Factories
{
	public class ShoppingAggregateRootFactory : IShoppingAggregateRootFactory
	{
		private readonly IBasketFactory _basketFactory;
		private readonly IOrderFactory _orderFactory;

		public ShoppingAggregateRootFactory(
			IBasketFactory productFactory,
			IOrderFactory orderFactory)
		{
			_basketFactory = productFactory;
			_orderFactory = orderFactory;
		}

		public async Task<ShoppingAggregateRoot> Create(string userName)
		{
			BasketDataTransferObject? basket = null;

			try
			{
				basket = await _basketFactory.Create(userName);
			}
			catch (Exception)
			{

			}

			IEnumerable<OrderDataTransferObject> orders = await _orderFactory.Create(userName);

			return new ShoppingAggregateRoot(
				userName,
				orders,
				basket);
		}
	}
}
