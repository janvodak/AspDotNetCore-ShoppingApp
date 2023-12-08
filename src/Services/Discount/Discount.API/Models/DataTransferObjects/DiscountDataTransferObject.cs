namespace ShoppingApp.Services.Discount.API.Models.DataTransferObjects
{
	public class DiscountDataTransferObject
	{
		public DiscountDataTransferObject(
			int id,
			string productName,
			string description,
			int amount)
		{
			Id = id;
			ProductName = productName;
			Description = description;
			Amount = amount;
		}

		public int Id { get; set; }

		public string ProductName { get; set; }

		public string Description { get; set; }

		public int Amount { get; set; }
	}
}
