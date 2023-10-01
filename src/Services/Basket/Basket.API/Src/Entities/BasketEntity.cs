namespace Basket.API.Src.Entities
{
	public class BasketEntity
	{
		public string UserName { get; set; } = null!;

		public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();

		public BasketEntity()
		{
		}

		public BasketEntity(string userName)
		{
			this.UserName = userName;
		}

		public decimal TotalPrice
		{
			get
			{
				decimal totalPrice = 0;

				foreach (var product in this.Products)
				{
					totalPrice += product.Price * product.Quantity;
				}

				return totalPrice;
			}
		}
	}
}

