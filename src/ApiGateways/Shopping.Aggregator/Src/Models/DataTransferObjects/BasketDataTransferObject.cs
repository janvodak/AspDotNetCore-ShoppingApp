namespace Shopping.Aggregator.Src.Models.DataTransferObjects
{
	public class BasketDataTransferObject
	{
		public BasketDataTransferObject(
			string userName,
			List<BasketProductDataTransferObject> products,
			decimal totalPrice)
		{
			UserName = userName;
			Products = products;
			TotalPrice = totalPrice;
		}

		public string UserName { get; set; }

		public List<BasketProductDataTransferObject> Products { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
