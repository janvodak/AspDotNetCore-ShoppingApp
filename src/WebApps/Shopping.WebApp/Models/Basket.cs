namespace Shopping.WebApp.Models
{
	public class Basket
	{
		public const string DEFAULT_USER_NAME = "swn";
		public Basket()
		{
			this.UserName = Basket.DEFAULT_USER_NAME;
			this.Products = new List<BasketProduct>();
			this.TotalPrice = 0;
		}

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

