using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.Utilities;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features
{
	public class HttpRequestMessageFactory : IHttpRequestMessageFactory
	{
		public HttpRequestMessage Create(RequestDataTransferObject request)
		{
			HttpRequestMessage message = new()
			{
				Method = request.ApiType switch
				{
					ApiTypeEnum.POST => HttpMethod.Post,
					ApiTypeEnum.DELETE => HttpMethod.Delete,
					ApiTypeEnum.PUT => HttpMethod.Put,
					_ => HttpMethod.Get,
				}
			};

			try
			{
				message.Headers.Add("Accept", "application/json");
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Unable to add request headers due to reason: '{ex.Message}'", ex);
			}

			try
			{
				message.RequestUri = new Uri(request.Url);
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Unable to create URI due to reason: '{ex.Message}'", ex);
			}

			if (request.Data == null)
			{
				return message;
			}

			string dataAsString;

			try
			{
				dataAsString = JsonSerializer.Serialize(request.Data);
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Unable to serialize data due to reason: '{ex.Message}'", ex);
			}

			MediaTypeHeaderValue mediaTypeHeaderValue = new("application/json");

			message.Content = new StringContent(dataAsString, Encoding.UTF8, mediaTypeHeaderValue);

			return message;
		}
	}
}
