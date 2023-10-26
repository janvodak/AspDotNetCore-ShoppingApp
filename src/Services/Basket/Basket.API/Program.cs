using System.Reflection;
using Basket.API.Src.GrpcServices;
using Basket.API.Src.Repositories;
using Discount.Grpc.Src.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

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
	//congurator.SetKebabCaseEndpointNameFormatter();
	//congurator.SetInMemorySagaRepositoryProvider();

	//var entryAssembly = Assembly.GetEntryAssembly();

	//congurator.AddConsumers(entryAssembly);
	//congurator.AddSagaStateMachines(entryAssembly);
	//congurator.AddSagas(entryAssembly);
	//congurator.AddActivities(entryAssembly);

	string host = builder.Configuration.GetValue<string>("EventBusSettings:HostAddress")
		?? throw new ArgumentNullException("EventBusSettings:HostAddress", "value is missing in appsettings.json");
	string username = builder.Configuration.GetValue<string>("EventBusSettings:Username")
		?? throw new ArgumentNullException("EventBusSettings:Username", "value is missing in appsettings.json");
	string password = builder.Configuration.GetValue<string>("EventBusSettings:Password")
		?? throw new ArgumentNullException("EventBusSettings:Password", "value is missing in appsettings.json");

	congurator.UsingRabbitMq((ctx, cfg) =>
	{
		cfg.Host(host, "/", h =>
		{
			h.Username(username);
			h.Password(password);
		});

		cfg.ConfigureEndpoints(ctx);
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
