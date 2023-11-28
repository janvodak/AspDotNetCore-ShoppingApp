using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Models
{
	public class ShoppingAggregateRoot
	{
		public ShoppingAggregateRoot(
			string userName,
			IEnumerable<OrderDataTransferObject> orders,
			BasketDataTransferObject? basket = null)
		{
			this.UserName = userName;
			this.Orders = orders;
			this.Basket = basket;
		}

		public string UserName { get; set; }

		public IEnumerable<OrderDataTransferObject> Orders { get; set; }

		public BasketDataTransferObject? Basket { get; set; } = null;
	}
}
