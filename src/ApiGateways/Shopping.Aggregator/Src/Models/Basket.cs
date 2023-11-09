namespace Shopping.Aggregator.Src.Models
{
	public class Basket
	{
		public Basket(
			string userName,
			List<BasketProduct> products,
			decimal totalPrice)
		{
			this.UserName = userName;
			this.Products = products;
			this.TotalPrice = totalPrice;
		}

		public string UserName { get; set; }

		public List<BasketProduct> Products { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
