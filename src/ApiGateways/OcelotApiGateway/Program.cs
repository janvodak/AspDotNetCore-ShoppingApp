using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using ShoppingApp.Components.Logger;

internal class Program
{
	private static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	private static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder
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
			})
			.UseSerilog((hostingContext, loggerConfiguration) =>
			{
				SeriLogger.Configure(hostingContext, loggerConfiguration);
			});
}
