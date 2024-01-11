using Microsoft.AspNetCore.Identity;
using Serilog;
using ShoppingApp.Components.Logger;
using ShoppingApp.Services.Authentication.API.Data;
using ShoppingApp.Services.Authentication.API.Models;
using ShoppingApp.Services.Authentication.API.Models.Factories;
using ShoppingApp.Services.Authentication.API.Repositories;
using ShoppingApp.Services.Authentication.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog logger component to use distribbuted logging with Kibana stack
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection(DatabaseSettings.SECTION_NAME));
builder.Services.Configure<JwtOptions>(
	builder.Configuration.GetSection(JwtOptions.SECTION_NAME));

builder.Services.AddDbContext<AuthenticationDbContext>();
builder.Services.AddScoped<AuthenticationDbContextMigration>();

builder.Services.AddScoped<IAuthenticationUserFactory, AuthenticationUserFactory>();

builder.Services.AddScoped<IRoleManagerRepository, RoleManagerRepository>();
builder.Services.AddScoped<IUserManagerRepository, UserManagerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAssignRoleService, AssignRoleService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegistrationService, RegisterService>();

// Add Default Identity Configuration
builder.Services
	.AddIdentity<AuthenticationUser, IdentityRole>()
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
