using AutoMapper;
using ShoppingApp.Services.Discount.Grpc.Data;
using ShoppingApp.Services.Discount.Grpc.Mappings;
using ShoppingApp.Services.Discount.Grpc.Repositories;
using ShoppingApp.Services.Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection(DatabaseSettings.SECTION_NAME));

builder.Services.AddScoped<DiscountContext>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
