using Shopping.Aggregator.Src.Features;
using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Services
{
	public class BaseService : IBaseService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;

		public BaseService(
			IHttpClientFactory httpClientFactory,
			IHttpRequestMessageFactory httpRequestMessageFactory)
		{
			_httpClientFactory = httpClientFactory;
			_httpRequestMessageFactory = httpRequestMessageFactory;
		}

		public async Task<HttpResponseMessage> SendAsync(
			string clientName,
			RequestDataTransferObject request)
		{
			HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
			HttpRequestMessage httpRequestMessage = _httpRequestMessageFactory.Create(request);

			try
			{
				return await httpClient.SendAsync(httpRequestMessage);
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Unable to send client request due to reason: '{ex.Message}'", ex);
			}
		}
	}
}
