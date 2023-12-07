using Microsoft.AspNetCore.Identity;
using ShoppingApp.Services.Authentication.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection(DatabaseSettings.SECTION_NAME));

builder.Services.AddDbContext<AuthenticationDbContext>();
builder.Services.AddScoped<AuthenticationDbContextMigration>();

// Add Default Identity Configuration
builder.Services
	.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<AuthenticationDbContext>()
	.AddDefaultTokenProviders();

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

	// Migrate the database - just for the testing purpose
	using (var scope = app.Services.CreateScope())
	{
		AuthenticationDbContextMigration _authenticationDbContextMigration = scope.ServiceProvider.GetRequiredService<AuthenticationDbContextMigration>();
		await _authenticationDbContextMigration.MigrateAsync();
	}
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
