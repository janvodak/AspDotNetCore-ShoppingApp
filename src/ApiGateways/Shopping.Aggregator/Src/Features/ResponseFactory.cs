using System.Net;

namespace Shopping.Aggregator.Src.Features
{
	public class ResponseFactory : IResponseFactory
	{
		private readonly IHttpResponseParser _httpResponseParser;

		public ResponseFactory(IHttpResponseParser httpResponseParser)
		{
			_httpResponseParser = httpResponseParser;
		}

		public async Task<T> Create<T>(HttpResponseMessage httpResponseMessage)
		{
			switch (httpResponseMessage.StatusCode)
			{
				case HttpStatusCode.NotFound:
					throw new ApplicationException("Not found");
				case HttpStatusCode.Forbidden:
					throw new ApplicationException("Access Denied");
				case HttpStatusCode.Unauthorized:
					throw new ApplicationException("Unauthorized");
				case HttpStatusCode.InternalServerError:
					throw new ApplicationException("Internal Server Error");
				default:
					break;
			}

			if (httpResponseMessage.IsSuccessStatusCode == false)
			{
				string? uri = httpResponseMessage.RequestMessage?.RequestUri?.AbsoluteUri;
				string? reason = httpResponseMessage.ReasonPhrase;

				throw new ApplicationException($"Something went wrong calling the API: '{uri}' for the reason: '{reason}'");
			}

			return await _httpResponseParser.ParseResponse<T>(httpResponseMessage);
		}
	}
}
