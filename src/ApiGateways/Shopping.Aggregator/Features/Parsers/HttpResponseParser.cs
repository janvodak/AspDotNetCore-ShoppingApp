using System.Text.Json;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Parsers
{
	public class HttpResponseParser : IHttpResponseParser
	{
		public async Task<T> ParseResponse<T>(HttpResponseMessage response)
		{
			JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

			string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			try
			{
				return JsonSerializer.Deserialize<T>(dataAsString, options)
					?? throw new InvalidDataException($"Unable to deserialize JSON data '{dataAsString}'");
			}
			catch (Exception ex)
			{
				throw new InvalidDataException(
					$"Unable to deserialize JSON data '{dataAsString}' due to reason: '{ex.Message}'",
					ex);
			}

		}
	}
}
