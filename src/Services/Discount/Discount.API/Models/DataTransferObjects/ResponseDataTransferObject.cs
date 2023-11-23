namespace ShoppingApp.Services.Discount.API.Models.DataTransferObjects
{
	public class ResponseDataTransferObject
	{
		public ResponseDataTransferObject()
		{
			Result = null;
			IsSuccess = true;
			Message = "Success";
		}

		public ResponseDataTransferObject(
			object? result,
			bool isSuccess,
			string message)
		{
			Result = result;
			IsSuccess = isSuccess;
			Message = message;
		}

		public object? Result { get; set; }
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}
