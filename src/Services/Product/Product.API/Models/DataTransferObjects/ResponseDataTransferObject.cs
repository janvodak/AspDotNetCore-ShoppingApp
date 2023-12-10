namespace ShoppingApp.Services.Product.API.Models.DataTransferObjects
{
	public class ResponseDataTransferObject
	{
		public ResponseDataTransferObject()
		{
			IsSuccess = true;
			Message = "Success";
			Result = null;
		}

		public ResponseDataTransferObject(
			bool isSuccess,
			string message,
			object? result = null)
		{
			IsSuccess = isSuccess;
			Message = message;
			Result = result;
		}

		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public object? Result { get; set; }
	}
}
