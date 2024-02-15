using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Shopping.WebApp.Configuration;
using Shopping.WebApp.Configuration.DataTransferObjects;
using Shopping.WebApp.Features;
using Shopping.WebApp.Services;
using ShoppingApp.Components.Logger;
using ShoppingApp.Components.Logger.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog logger component to use distribbuted logging with Kibana stack
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.
builder.Services.AddTransient<ExternalRecordLoggerDelegatingHandler>();
builder.Services.AddScoped<JsonResponseParser>();
builder.Services.AddScoped<JsonRequestFactory>();

ApiServicesSettings apiServicesSettings = new();
builder.Configuration.GetSection(ApiServicesSettings.SECTION_NAME).Bind(apiServicesSettings);

// Read policy options from configuration
builder.Services.Configure<CircuitBreakerPolicySettings>(
	builder.Configuration.GetSection(CircuitBreakerPolicySettings.SECTION_NAME));
builder.Services.Configure<RetryPolicySettings>(
	builder.Configuration.GetSection(RetryPolicySettings.SECTION_NAME));

//Add http client services at ConfigureServices(IServiceCollection services)
builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OcelotApiGatewayUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(RetryPolicyConfiguration.Create<BasketApiService>(builder.Configuration))
	.AddPolicyHandler(CircuitBreakerPolicyConfiguration.Create(builder.Configuration));

builder.Services.AddHttpClient<IBasketApiService, BasketApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OcelotApiGatewayUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(RetryPolicyConfiguration.Create<BasketApiService>(builder.Configuration))
	.AddPolicyHandler(CircuitBreakerPolicyConfiguration.Create(builder.Configuration));

builder.Services.AddHttpClient<IOrderApiService, OrderApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OcelotApiGatewayUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(RetryPolicyConfiguration.Create<BasketApiService>(builder.Configuration))
	.AddPolicyHandler(CircuitBreakerPolicyConfiguration.Create(builder.Configuration));

builder.Services.AddRazorPages();

builder.Services.ConfigureHealthChecks(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapHealthChecks("/health/live", new HealthCheckOptions()
{
	Predicate = _ => false,
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
	ResultStatusCodes =
	{
		[HealthStatus.Healthy] = StatusCodes.Status200OK,
		[HealthStatus.Degraded] = StatusCodes.Status200OK,
		[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
	}
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions()
{
	Predicate = (check) => check.Tags.Contains("ready"),
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
	ResultStatusCodes =
	{
		[HealthStatus.Healthy] = StatusCodes.Status200OK,
		[HealthStatus.Degraded] = StatusCodes.Status200OK,
		[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
	}
});

app.Run();
