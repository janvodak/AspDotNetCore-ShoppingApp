namespace ShoppingApp.Services.Order.API.Rest.Models.DataTransferObjects
{
	public record ResponseDataTransferObject
	{
		public ResponseDataTransferObject()
		{
			IsSuccess = true;
			Message = "Success";
			Result = null;
		}

		public ResponseDataTransferObject(object result)
		{
			IsSuccess = true;
			Message = "Success";
			Result = result;
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

		public bool IsSuccess { get; init; }
		public string Message { get; init; }
		public object? Result { get; init; }
	}
}
