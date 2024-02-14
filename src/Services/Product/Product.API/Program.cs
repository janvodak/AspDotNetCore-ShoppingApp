using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using ShoppingApp.Components.Logger;
using ShoppingApp.Services.Product.API.Configuration;
using ShoppingApp.Services.Product.API.Configuration.DataTransferObjects;
using ShoppingApp.Services.Product.API.Data;
using ShoppingApp.Services.Product.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog logger component to use distribbuted logging with Kibana stack
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductDbContext, ProductDbContext>();

builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection(DatabaseSettings.SECTION_NAME));

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
}

app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions() {
	Predicate = _ => true,
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
