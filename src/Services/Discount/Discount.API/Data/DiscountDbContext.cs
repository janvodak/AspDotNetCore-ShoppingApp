using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Discount.API.Data.Configuration;
using ShoppingApp.Services.Discount.API.Data.Policies;
using ShoppingApp.Services.Discount.API.Models;

namespace ShoppingApp.Services.Discount.API.Data
{
	public partial class DiscountDbContext : DbContext
	{
		private readonly DatabaseSettings _databaseSettings;
		private readonly EntityFrameworkPolicySettings _entityFrameworkPolicySettings;

		public DiscountDbContext(
			IOptions<DatabaseSettings> databaseSettings,
			IOptions<EntityFrameworkPolicySettings> entityFrameworkPolicySettings)
		{
			_databaseSettings = databaseSettings.Value;
			_entityFrameworkPolicySettings = entityFrameworkPolicySettings.Value;
		}

		public virtual DbSet<DiscountModel> Discounts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseNpgsql(
				_databaseSettings.GetConnectionString(),
				npgsqlOptionsAction: sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(
						maxRetryCount: _entityFrameworkPolicySettings.MaxRetryCount,
						maxRetryDelay: TimeSpan.FromSeconds(_entityFrameworkPolicySettings.MaxRetryDelay),
						errorCodesToAdd: null);
				});
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<DiscountModel>().HasData(new DiscountModel(1, "IPhone X", "IPhone Discount", 150));
			//modelBuilder.Entity<DiscountModel>().HasData(new DiscountModel(2, "Samsung 10", "Samsung Discount", 100));
		}
	}
}
