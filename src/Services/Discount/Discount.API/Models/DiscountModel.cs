using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Services.Discount.API.Models
{
	public class DiscountModel
	{
		public DiscountModel(
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

		[Key]
		public int Id { get; set; }

		[Required]
		public string ProductName { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public int Amount { get; set; }
	}
}
