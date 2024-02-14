using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using ShoppingApp.Components.Logger;
using ShoppingApp.Services.Discount.API.Configuration;
using ShoppingApp.Services.Discount.API.Configuration.DataTransferObjects;
using ShoppingApp.Services.Discount.API.Data;
using ShoppingApp.Services.Discount.API.Data.Policies;
using ShoppingApp.Services.Discount.API.Models.DataTransferObjects.Factories;
using ShoppingApp.Services.Discount.API.Models.Mappings;
using ShoppingApp.Services.Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog logger component to use distribbuted logging with Kibana stack
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection(DatabaseSettings.SECTION_NAME));
builder.Services.Configure<EntityFrameworkPolicySettings>(
	builder.Configuration.GetSection(EntityFrameworkPolicySettings.SECTION_NAME));
builder.Services.Configure<PollyPolicySettings>(
	builder.Configuration.GetSection(PollyPolicySettings.SECTION_NAME));

builder.Services.AddScoped<SingleDiscountResponseFactory>();
builder.Services.AddScoped<MultipleDiscountsResponseFactory>();

builder.Services.AddScoped<DiscountDbContext>();

builder.Services.AddScoped<PollyyRetryPolicyFactory>();
builder.Services.AddScoped<DiscountDbContextMigration>(provider =>
	ActivatorUtilities.CreateInstance<DiscountDbContextMigration>(
		provider,
		provider.GetRequiredService<PollyyRetryPolicyFactory>().Create()));

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

	// Migrate the database and seed it - just for the testing purpose
	using (var scope = app.Services.CreateScope())
	{
		DiscountDbContextMigration _discountContextSeedService = scope.ServiceProvider.GetRequiredService<DiscountDbContextMigration>();
		_discountContextSeedService.Migrate();
	}
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
