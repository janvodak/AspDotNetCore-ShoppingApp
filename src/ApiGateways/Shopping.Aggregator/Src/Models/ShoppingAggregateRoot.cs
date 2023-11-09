namespace Shopping.Aggregator.Src.Models
{
	public class ShoppingAggregateRoot
	{
		public ShoppingAggregateRoot(
			string userName,
			Basket basket,
			IEnumerable<Order> orders)
		{
			this.UserName = userName;
			this.Basket = basket;
			this.Orders = orders;
		}

		public string UserName { get; set; }

		public Basket Basket { get; set; }

		public IEnumerable<Order> Orders { get; set; }
	}
}
