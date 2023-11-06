using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

internal class Program
{
	private static void Main(string[] args)
	{
		CreateHostBuilder().Build().Run();
	}

	private static IWebHostBuilder CreateHostBuilder() =>
		new WebHostBuilder()
			.UseKestrel()
			.UseContentRoot(Directory.GetCurrentDirectory())
			.ConfigureAppConfiguration((hostingContext, config) =>
			{
				config
					.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
					.AddJsonFile("appsettings.json", true, true)
					.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
					.AddJsonFile("ocelot.json")
					.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
					.AddEnvironmentVariables();
			})
			.ConfigureServices(services =>
			{
				services.AddOcelot()
					.AddCacheManager(settings => settings.WithDictionaryHandle());
			})
			.ConfigureLogging((hostingContext, logging) =>
			{
				logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
				logging.AddConsole();
				logging.AddDebug();
			})
			.UseIISIntegration()
			.Configure(app =>
			{
				app.UseRouting();

				app.UseEndpoints(endpoints =>
				{
					endpoints.MapGet("/", async context =>
					{
						await context.Response.WriteAsync("Hello World!");
					});
				});

				app.UseOcelot().Wait();
			});
}
