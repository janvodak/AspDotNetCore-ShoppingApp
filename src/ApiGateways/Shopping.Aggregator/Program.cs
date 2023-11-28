using Shopping.Aggregator.Src.Factories;
using Shopping.Aggregator.Src.Features;
using Shopping.Aggregator.Src.Services;
using Shopping.Aggregator.Src.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<BasketFactory>();
builder.Services.AddScoped<OrderFactory>();
builder.Services.AddScoped<ShoppingAggregateRootFactory>();

builder.Services.AddScoped<IHttpRequestMessageFactory, HttpRequestMessageFactory>();
builder.Services.AddScoped<IHttpResponseParser, HttpResponseParser>();
builder.Services.AddScoped<IResponseFactory, ResponseFactory>();

ApiServicesSettings apiServicesSettings = new();
builder.Configuration.GetSection(ApiServicesSettings.NAME_OF_SECTION).Bind(apiServicesSettings);

//Add http client services at ConfigureServices(IServiceCollection services)
builder.Services.AddScoped<IBaseService, BaseService>();

builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.ProductApiUrl);
});

builder.Services.AddHttpClient<IBasketApiService, BasketApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.BasketApiUrl);
});

builder.Services.AddHttpClient<IOrderApiService, OrderApiService>(client =>
{
	client.BaseAddress = new Uri(apiServicesSettings.OrderApiUrl);
});

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

