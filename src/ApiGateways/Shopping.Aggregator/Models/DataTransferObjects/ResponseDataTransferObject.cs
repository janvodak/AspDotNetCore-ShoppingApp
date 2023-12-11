namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects
{
	public class ResponseDataTransferObject<T> where T : class
	{
		public ResponseDataTransferObject()
		{
			Message = "Success";
			IsSuccess = true;
			Result = null;
		}

		public ResponseDataTransferObject(
			string message,
			bool isSuccess,
			T? result = null)
		{
			Message = message;
			IsSuccess = isSuccess;
			Result = result;
		}

		public string Message { get; set; }
		public bool IsSuccess { get; set; }
		public T? Result { get; set; }
	}
}

