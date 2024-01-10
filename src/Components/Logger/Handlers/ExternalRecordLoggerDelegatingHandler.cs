using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace ShoppingApp.Components.Logger.Handlers
{
	public class ExternalRecordLoggerDelegatingHandler : DelegatingHandler
	{
		private readonly ILogger<ExternalRecordLoggerDelegatingHandler> logger;

		public ExternalRecordLoggerDelegatingHandler(ILogger<ExternalRecordLoggerDelegatingHandler> logger)
		{
			this.logger = logger;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Sending external request: '{@Request}'.", request);

			HttpResponseMessage response;

			try
			{
				response = await base.SendAsync(request, cancellationToken);
			}
			catch (HttpRequestException ex)
				when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionRefused)
			{
				string hostWithPort = request.RequestUri!.IsDefaultPort
					? request.RequestUri.DnsSafeHost
					: $"{request.RequestUri.DnsSafeHost}:{request.RequestUri.Port}";

				logger.LogCritical(
					ex,
					"Unable to connect to {Host}. Please check the configuration to ensure the correct URL for the service has been configured.",
					hostWithPort);

				return new HttpResponseMessage(HttpStatusCode.BadGateway)
				{
					RequestMessage = request
				};
			}

			if (response.IsSuccessStatusCode == true)
			{
				logger.LogInformation(
					"Received a success external response: '{@Response}'.",
					response);
			}
			else
			{
				logger.LogWarning(
					"Received a non-success status code '{StatusCode}'. Response: '{@Response}'.",
					(int)response.StatusCode,
					response);
			}

			return response;	
		}
	}
}
