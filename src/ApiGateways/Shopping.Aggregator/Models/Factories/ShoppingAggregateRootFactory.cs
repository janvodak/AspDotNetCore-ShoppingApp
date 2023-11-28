using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects.Factories;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.Factories
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
