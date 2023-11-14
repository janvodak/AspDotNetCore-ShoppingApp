using System.Net.Http.Headers;
using System.Text.Json;

namespace Shopping.WebApp.Features
{
	public class JsonRequestFactory
	{
		public StringContent CreateNewApiRequest<T>(T data)
		{
			var dataAsString = JsonSerializer.Serialize(data);

			var content = new StringContent(dataAsString);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return content;
		}
	}
}
