using System.Text.Json;

namespace Shopping.WebApp.Features
{
	public class JsonResponseParser
	{
		public void ValidateResponse(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode == false)
			{
				string? uri = response.RequestMessage?.RequestUri?.AbsoluteUri;
				string? reason = response.ReasonPhrase;
				string message = $"Something went wrong calling the API: '{uri}' for the reason: '{reason}'";

				throw new ApplicationException(message);
			}
		}

		public async Task<T> ParseResponse<T>(HttpResponseMessage response)
		{
			this.ValidateResponse(response);

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

