namespace ShoppingApp.Services.Discount.API.Models.DataTransferObjects.Factories
{
	public class MultipleDiscountsResponseFactory
	{
		public ResponseDataTransferObject CreateEnumerableDiscount(IEnumerable<DiscountDataTransferObject> discountDataTransferObjects)
		{
			ResponseDataTransferObject response = new()
			{
				Result = discountDataTransferObjects
			};

			if (discountDataTransferObjects.Any() == false)
			{
				response.IsSuccess = true;
				response.Message = "There are no discounts available.";
			}

			return response;
		}
	}
}
