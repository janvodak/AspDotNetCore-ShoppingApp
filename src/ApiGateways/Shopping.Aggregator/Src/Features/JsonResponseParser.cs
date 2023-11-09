using System.Text.Json;

namespace Shopping.Aggregator.Src.Features
{
	public class JsonResponseParser
	{
		public async Task<T> Parse<T>(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode == false)
			{
				string? uri = response.RequestMessage?.RequestUri?.AbsoluteUri;
				string? reason = response.ReasonPhrase;
				string message = $"Something went wrong calling the API: '{uri}' for the reason: '{reason}'";

				throw new ApplicationException(message);
			}

			JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

			string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			T? deserializedResponse = JsonSerializer.Deserialize<T>(dataAsString, options);

			if (deserializedResponse == null)
			{
				throw new InvalidDataException($"Unable to deserialize JSON data '{dataAsString}'");
			}

			return deserializedResponse;
		}
	}
}
