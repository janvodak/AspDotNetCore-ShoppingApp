using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Authentication.API.Models;

namespace ShoppingApp.Services.Authentication.API.Data
{
	public partial class AuthenticationDbContext : IdentityDbContext<AuthenticationUser>
	{
		private readonly IOptions<DatabaseSettings> _databaseSettings;

		public AuthenticationDbContext(IOptions<DatabaseSettings> databaseSettings)
		{
			_databaseSettings = databaseSettings;
		}

		public DbSet<AuthenticationUser> AuthenticationUsers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseSqlServer(_databaseSettings.Value.GetConnectionString());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
