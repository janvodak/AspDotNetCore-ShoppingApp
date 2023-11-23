using AutoMapper;
using ShoppingApp.Services.Discount.API.Data;
using ShoppingApp.Services.Discount.API.Mappings;
using ShoppingApp.Services.Discount.API.Models.DataTransferObjects.Factories;
using ShoppingApp.Services.Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection(DatabaseSettings.SECTION_NAME));

builder.Services.AddScoped<SingleDiscountResponseFactory>();
builder.Services.AddScoped<MultipleDiscountsResponseFactory>();

builder.Services.AddScoped<DiscountContext>();
builder.Services.AddScoped<DiscountContextSeed>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
	using (var scope = app.Services.CreateScope())
	{
		DiscountContextSeed _discountContextSeedService = scope.ServiceProvider.GetRequiredService<DiscountContextSeed>();
		await _discountContextSeedService.SeedAsync();
	}
}

app.UseAuthorization();
app.MapControllers();
app.Run();
