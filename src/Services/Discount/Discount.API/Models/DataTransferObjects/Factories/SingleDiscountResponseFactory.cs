namespace ShoppingApp.Services.Discount.API.Models.DataTransferObjects.Factories
{
	public class SingleDiscountResponseFactory
	{
		public ResponseDataTransferObject Create(
			DiscountDataTransferObject? discountDataTransferObject,
			string productName)
		{
			ResponseDataTransferObject response = new()
			{
				Result = discountDataTransferObject
			};

			if (discountDataTransferObject == null)
			{
				response.IsSuccess = true;
				response.Message = $"There is no discount for a product '{productName}'.";
			}

			return response;
		}
	}
}
