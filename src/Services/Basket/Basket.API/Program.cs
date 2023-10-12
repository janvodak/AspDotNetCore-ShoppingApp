using Basket.API.Src.GrpcServices;
using Basket.API.Src.Repositories;
using Discount.Grpc.Src.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddGrpcClient<GetDiscountProtocolBufferService.GetDiscountProtocolBufferServiceClient>(options =>
{
	options.Address = new Uri(uriString: builder.Configuration.GetValue<string>("DiscountGrpcSettings:DiscountUrl"));
});

builder.Services.AddScoped<GetDiscountGrpcService>();

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
