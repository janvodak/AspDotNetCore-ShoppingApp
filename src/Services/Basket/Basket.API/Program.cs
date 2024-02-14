using System.Reflection;
using Basket.API.Src.Configuration;
using Basket.API.Src.GrpcServices;
using Basket.API.Src.Publishers;
using Basket.API.Src.Repositories;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using ShoppingApp.Components.Logger;
using ShoppingApp.Services.Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog logger component to use distribbuted logging with Kibana stack
builder.Host.UseSerilog(SeriLogger.Configure);

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
	Predicate = (check) => check.Tags.Contains("ready") || check.Tags.Contains("db"),
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
	ResultStatusCodes =
	{
		[HealthStatus.Healthy] = StatusCodes.Status200OK,
		[HealthStatus.Degraded] = StatusCodes.Status200OK,
		[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
	}
});

app.Run();
