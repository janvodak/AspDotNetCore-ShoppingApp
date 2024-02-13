using Serilog;
using Shopping.WebApp.Features;
using Shopping.WebApp.Policies;
using Shopping.WebApp.Policies.Configuration;
using Shopping.WebApp.Services;
using Shopping.WebApp.Settings;
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
builder.Configuration.GetSection(ApiServicesSettings.NAME_OF_SECTION).Bind(apiServicesSettings);

// Read policy options from configuration
builder.Services.Configure<CircuitBreakerPolicySettings>(
	builder.Configuration.GetSection(CircuitBreakerPolicySettings.SECTION_NAME));
builder.Services.Configure<RetryPolicySettings>(
	builder.Configuration.GetSection(RetryPolicySettings.SECTION_NAME));

builder.Services.AddScoped<CircuitBreakerPolicyFactory>();
builder.Services.AddScoped<RetryPolicyFactory>();

// @TODO this is terrible solution, it needs to be rewriten
CircuitBreakerPolicyFactory circuitBreakerPolicyFactory = builder.Services.BuildServiceProvider().GetRequiredService<CircuitBreakerPolicyFactory>();
RetryPolicyFactory retryPolicyFactory = builder.Services.BuildServiceProvider().GetRequiredService<RetryPolicyFactory>();

//Add http client services at ConfigureServices(IServiceCollection services)
builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OcelotApiGatewayUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(retryPolicyFactory.Create<ProductApiService>())
	.AddPolicyHandler(circuitBreakerPolicyFactory.Create());

builder.Services.AddHttpClient<IBasketApiService, BasketApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OcelotApiGatewayUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(retryPolicyFactory.Create<BasketApiService>())
	.AddPolicyHandler(circuitBreakerPolicyFactory.Create());

builder.Services.AddHttpClient<IOrderApiService, OrderApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OcelotApiGatewayUrl);
})
	.AddHttpMessageHandler<ExternalRecordLoggerDelegatingHandler>()
	.AddPolicyHandler(retryPolicyFactory.Create<OrderApiService>())
	.AddPolicyHandler(circuitBreakerPolicyFactory.Create());

builder.Services.AddRazorPages();

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

app.Run();
