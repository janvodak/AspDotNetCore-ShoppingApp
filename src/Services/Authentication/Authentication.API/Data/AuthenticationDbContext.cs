using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Authentication.API.Models;
using ShoppingApp.Services.Authentication.API.Settings;

namespace ShoppingApp.Services.Authentication.API.Data
{
	public partial class AuthenticationDbContext : IdentityDbContext<AuthenticationUser>
	{
		private readonly DatabaseSettings _databaseSettings;
		private readonly EntityFrameworkPolicySettings _entityFrameworkPolicySettings;

		public AuthenticationDbContext(
			IOptions<DatabaseSettings> databaseSettings,
			IOptions<EntityFrameworkPolicySettings> entityFrameworkPolicySettings)
		{
			_databaseSettings = databaseSettings.Value;
			_entityFrameworkPolicySettings = entityFrameworkPolicySettings.Value;
		}

		public DbSet<AuthenticationUser> AuthenticationUsers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseSqlServer(
				_databaseSettings.GetConnectionString(),
				sqlServerOptionsAction: sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(
						maxRetryCount: _entityFrameworkPolicySettings.MaxRetryCount,
						maxRetryDelay: TimeSpan.FromSeconds(_entityFrameworkPolicySettings.MaxRetryDelay),
						errorNumbersToAdd: null);
				});
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
