using Order.Application.Src;
using Order.Infrastructure.Src;
using Order.Infrastructure.Src.Persistence.Context;
using Order.Infrastructure.Src.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

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
