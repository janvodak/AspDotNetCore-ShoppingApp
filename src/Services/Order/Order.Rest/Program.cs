using System.Reflection;
using EventBus.Messages.Src.Shared;
using MassTransit;
using Order.Application.Src;
using Order.Infrastructure.Src;
using Order.Infrastructure.Src.Persistence.Context;
using Order.Infrastructure.Src.Persistence.Extensions;
using Order.Rest.Src.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

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

	congurator.AddConsumer<BasketCheckoutConsumer>();

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

		cfg.ReceiveEndpoint(EventBusConstants.BASKET_CHECKOUT_QUEUE_NAME, c =>
		{
			c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
		});

		//cfg.ConfigureEndpoints(ctx);
	});
});

// General configuration
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<BasketCheckoutConsumer>();

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

app.Run();
