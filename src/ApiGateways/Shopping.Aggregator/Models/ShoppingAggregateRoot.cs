using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models
{
	public class ShoppingAggregateRoot
	{
		public ShoppingAggregateRoot(
			string userName,
			IEnumerable<OrderDataTransferObject> orders,
			BasketDataTransferObject? basket = null)
		{
			UserName = userName;
			Orders = orders;
			Basket = basket;
		}

		public string UserName { get; set; }

		public IEnumerable<OrderDataTransferObject> Orders { get; set; }

		public BasketDataTransferObject? Basket { get; set; } = null;
	}
}
