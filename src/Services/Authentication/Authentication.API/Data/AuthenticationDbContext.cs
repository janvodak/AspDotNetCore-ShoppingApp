using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ShoppingApp.Services.Authentication.API.Data
{
	public partial class AuthenticationDbContext : IdentityDbContext<IdentityUser>
	{
		private readonly IOptions<DatabaseSettings> _databaseSettings;

		public AuthenticationDbContext(IOptions<DatabaseSettings> databaseSettings)
		{
			_databaseSettings = databaseSettings;
		}

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
