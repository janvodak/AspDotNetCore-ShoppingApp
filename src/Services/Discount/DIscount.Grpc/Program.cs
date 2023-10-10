﻿using Discount.Grpc.Src.Data;
using Discount.Grpc.Src.Repositories;
using Discount.Grpc.Src.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddScoped<DiscountContext>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddGrpc();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<DiscountContext>();
	//context.Database.EnsureCreated();
	// DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
app.MapGrpcService<CreateDiscoutService>();
app.MapGrpcService<DeleteDiscoutService>();
app.MapGrpcService<GetDiscoutService>();
app.MapGrpcService<UpdateDiscoutService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
