using Shopping.Aggregator.Src.Models.Utilities;

namespace Shopping.Aggregator.Src.Models.DataTransferObjects
{
	public class RequestDataTransferObject
	{
		public RequestDataTransferObject(
			string url,
			ApiTypeEnum apiType = ApiTypeEnum.GET,
			ContentTypeEnum contentType = ContentTypeEnum.Json,
			object? data = null,
			string? accessToken = null)
		{
			Url = url;
			ApiType = apiType;
			ContentType = contentType;
			Data = data;
			AccessToken = accessToken;
		}

		public string Url { get; set; }

		public ApiTypeEnum ApiType { get; set; }

		public ContentTypeEnum ContentType { get; set; }

		public object? Data { get; set; }

		public string? AccessToken { get; set; }
	}
}
