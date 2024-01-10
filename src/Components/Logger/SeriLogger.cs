using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace ShoppingApp.Components.Logger
{
	public static class SeriLogger
	{
		public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
		   (context, configuration) =>
		   {
			   string elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri")
						   ?? throw new ArgumentNullException("Value 'ElasticConfiguration:Uri' is missing in appsettings.json.");

			   string? formattedApplicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
			   string? formattedEnvironmentName = context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-");
			   string formattedTimestamp = DateTime.UtcNow.ToString("yyyy-MM");

			   string indexFormat = String.Format("{0}-{1}-{2}-{3}",
				   "applogs",
				   formattedApplicationName,
				   formattedEnvironmentName,
				   formattedTimestamp);

			   configuration
					.Enrich.FromLogContext()
					.Enrich.WithMachineName()
					.WriteTo.Debug()
					.WriteTo.Console()
					.WriteTo.Elasticsearch(
						new ElasticsearchSinkOptions(new Uri(elasticUri))
						{
							IndexFormat = indexFormat,
							AutoRegisterTemplate = true,
							NumberOfShards = 2,
							NumberOfReplicas = 1
						})
					.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
					.Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
					.ReadFrom.Configuration(context.Configuration);
		   };
	}
}
