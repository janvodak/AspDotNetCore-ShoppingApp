using System.Reflection;
using Basket.API.Src.GrpcServices;
using Basket.API.Src.Publishers;
using Basket.API.Src.Repositories;
using Discount.Grpc.Src.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketCheckoutEventPublisher, BasketCheckoutEventPublisher>();

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// Discount gRPC Configuration
builder.Services.AddGrpcClient<GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceClient>(options =>
{
	string? discountGrpcUriString = builder.Configuration.GetValue<string>("DiscountGrpcSettings:DiscountUrl")
		?? throw new ArgumentNullException(
			"DiscountGrpcSettings:DiscountUrl",
			"value is missing in appsettings.json");

	options.Address = new Uri(uriString: discountGrpcUriString);
});
builder.Services.AddScoped<GetDiscountGrpcService>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(congurator =>
{
	EventBusSettings eventBusSettings = new();
	builder.Configuration.GetSection(EventBusSettings.NAME_OF_SECTION).Bind(eventBusSettings);

	congurator.UsingRabbitMq((ctx, cfg) =>
	{
		cfg.Host(eventBusSettings.HostAddress, "/", h =>
		{
			h.Username(eventBusSettings.Username);
			h.Password(eventBusSettings.Password);
		});
	});
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
