namespace Shopping.Aggregator.Src.Models
{
	public class ShoppingAggregateRoot
	{
		public ShoppingAggregateRoot(
			string userName,
			IEnumerable<Order> orders,
			Basket? basket = null)
		{
			this.UserName = userName;
			this.Orders = orders;
			this.Basket = basket;
		}

		public string UserName { get; set; }

		public IEnumerable<Order> Orders { get; set; }

		public Basket? Basket { get; set; } = null;
	}
}
