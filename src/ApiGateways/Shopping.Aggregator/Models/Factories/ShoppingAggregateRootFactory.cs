using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.Factories
{
	public class ShoppingAggregateRootFactory : IShoppingAggregateRootFactory
	{
		private readonly IBasketHandler _basketHandler;
		private readonly IOrderHandler _orderHandler;

		public ShoppingAggregateRootFactory(
			IBasketHandler productHandler,
			IOrderHandler orderHandler)
		{
			_basketHandler = productHandler;
			_orderHandler = orderHandler;
		}

		public async Task<ShoppingAggregateRoot> Create(string userName)
		{
			BasketDataTransferObject? basket = null;

			try
			{
				basket = await _basketHandler.Handle(userName);
			}
			catch (Exception)
			{

			}

			IEnumerable<OrderDataTransferObject> orders = await _orderHandler.Handle(userName);

			return new ShoppingAggregateRoot(
				userName,
				orders,
				basket);
		}
	}
}
