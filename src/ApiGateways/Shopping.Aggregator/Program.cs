using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using ShoppingApp.ApiGateway.ShoppingAggregator.Configuration;
using ShoppingApp.ApiGateway.ShoppingAggregator.Configuration.DataTransferObjects;
using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Factories;
using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers;
using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Parsers;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.Factories;
using ShoppingApp.ApiGateway.ShoppingAggregator.Services;
using ShoppingApp.Components.Logger;
using ShoppingApp.Components.Logger.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog logger component to use distribbuted logging with Kibana stack
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.
builder.Services.AddTransient<ExternalRecordLoggerDelegatingHandler>();
builder.Services.AddScoped<IBasketHandler, BasketHandler>();
builder.Services.AddScoped<IOrderHandler, OrderHandler>();
builder.Services.AddScoped<IProductHandler, ProductHandler>();

builder.Services.AddScoped<IShoppingAggregateRootFactory, ShoppingAggregateRootFactory>();

builder.Services.AddScoped<IHttpRequestMessageFactory, HttpRequestMessageFactory>();
builder.Services.AddScoped<IHttpResponseParser, HttpResponseParser>();
builder.Services.AddScoped<IResponseFactory, ResponseFactory>();

ApiServicesSettings apiServicesSettings = new();
builder.Configuration.GetSection(ApiServicesSettings.SECTION_NAME).Bind(apiServicesSettings);

// Read policy options from configuration
builder.Services.Configure<CircuitBreakerPolicySettings>(
	builder.Configuration.GetSection(CircuitBreakerPolicySettings.SECTION_NAME));
builder.Services.Configure<RetryPolicySettings>(
	builder.Configuration.GetSection(RetryPolicySettings.SECTION_NAME));

//Add http client services at ConfigureServices(IServiceCollection services)
builder.Services.AddHttpClient<IBasketApiService, BasketApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.BasketApiUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(RetryPolicyConfiguration.Create<BasketApiService>(builder.Configuration))
	.AddPolicyHandler(CircuitBreakerPolicyConfiguration.Create(builder.Configuration));

builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.ProductApiUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(RetryPolicyConfiguration.Create<ProductApiService>(builder.Configuration))
.AddPolicyHandler(CircuitBreakerPolicyConfiguration.Create(builder.Configuration));

builder.Services.AddHttpClient<IOrderApiService, OrderApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OrderApiUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(RetryPolicyConfiguration.Create<OrderApiService>(builder.Configuration))
	.AddPolicyHandler(CircuitBreakerPolicyConfiguration.Create(builder.Configuration));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHealthChecks(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

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
