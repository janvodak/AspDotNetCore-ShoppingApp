namespace Shopping.Aggregator.Src.Models.DataTransferObjects
{
	public class ResponseDataTransferObject
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
			object? result = null)
		{
			Message = message;
			IsSuccess = isSuccess;
			Result = result;
		}

		public string Message { get; set; }
		public bool IsSuccess { get; set; }
		public object? Result { get; set; }
	}
}

