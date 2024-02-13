using Serilog;
using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Factories;
using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers;
using ShoppingApp.ApiGateway.ShoppingAggregator.Features.Parsers;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.Factories;
using ShoppingApp.ApiGateway.ShoppingAggregator.Policies;
using ShoppingApp.ApiGateway.ShoppingAggregator.Services;
using ShoppingApp.ApiGateway.ShoppingAggregator.Settings;
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
builder.Configuration.GetSection(ApiServicesSettings.NAME_OF_SECTION).Bind(apiServicesSettings);

// Read policy options from configuration
builder.Services.Configure<CircuitBreakerPolicySettings>(
	builder.Configuration.GetSection(CircuitBreakerPolicySettings.SECTION_NAME));
builder.Services.Configure<RetryPolicySettings>(
	builder.Configuration.GetSection(RetryPolicySettings.SECTION_NAME));

builder.Services.AddScoped<CircuitBreakerPolicyFactory>();
builder.Services.AddScoped<RetryPolicyFactory>();

// Retrieve policy-related classes from the DI container.
// @TODO this is terrible solution, it needs to be rewriten
CircuitBreakerPolicyFactory circuitBreakerPolicyFactory = builder.Services.BuildServiceProvider().GetRequiredService<CircuitBreakerPolicyFactory>();
RetryPolicyFactory retryPolicyFactory = builder.Services.BuildServiceProvider().GetRequiredService<RetryPolicyFactory>();

//Add http client services at ConfigureServices(IServiceCollection services)
builder.Services.AddHttpClient<IBasketApiService, BasketApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.BasketApiUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(retryPolicyFactory.Create<BasketApiService>())
	.AddPolicyHandler(circuitBreakerPolicyFactory.Create());

builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.ProductApiUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(retryPolicyFactory.Create<ProductApiService>())
	.AddPolicyHandler(circuitBreakerPolicyFactory.Create());

builder.Services.AddHttpClient<IOrderApiService, OrderApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OrderApiUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(retryPolicyFactory.Create<OrderApiService>())
	.AddPolicyHandler(circuitBreakerPolicyFactory.Create());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
