using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using ShoppingApp.Components.EventBus.Messages.Shared;
using ShoppingApp.Components.Logger;
using ShoppingApp.Services.Order.API.Application;
using ShoppingApp.Services.Order.API.Domain;
using ShoppingApp.Services.Order.API.Infrastructure;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Extensions;
using ShoppingApp.Services.Order.API.Rest.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog logger component to use distribbuted logging with Kibana stack
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.
builder.Services.AddDomainServices();
builder.Services.AddAplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// General configuration
builder.Services.AddScoped<BasketCheckoutConsumer>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(congurator =>
{
	EventBusSettings eventBusSettings = new();
	builder.Configuration.GetSection(EventBusSettings.NAME_OF_SECTION).Bind(eventBusSettings);

	congurator.AddConsumer<BasketCheckoutConsumer>();

	congurator.UsingRabbitMq((ctx, cfg) =>
	{
		cfg.Host(eventBusSettings.HostAddress, "/", h =>
		{
			h.Username(eventBusSettings.Username);
			h.Password(eventBusSettings.Password);
		});

		cfg.ReceiveEndpoint(EventBusConstants.BASKET_CHECKOUT_QUEUE_NAME, c =>
		{
			c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
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

	// Migrate the database and seed it - just for the testing purpose
	app.Services.MigrateDatabase<OrderContext>((context, serviceProvider) =>
	{
		OrderContextSeed.SeedAsync(context, serviceProvider.GetRequiredService<ILogger<OrderContextSeed>>()).Wait();
	});
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
