using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Discount.API.Models;

namespace ShoppingApp.Services.Discount.API.Data
{
	public partial class DiscountDbContext : DbContext
	{
		private readonly IOptions<DatabaseSettings> _databaseSettings;

		public DiscountDbContext(IOptions<DatabaseSettings> databaseSettings)
		{
			_databaseSettings = databaseSettings;
		}

		public virtual DbSet<DiscountModel> Discounts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseNpgsql(_databaseSettings.Value.GetConnectionString());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<DiscountModel>().HasData(new DiscountModel(1, "IPhone X", "IPhone Discount", 150));
			//modelBuilder.Entity<DiscountModel>().HasData(new DiscountModel(2, "Samsung 10", "Samsung Discount", 100));
		}
	}
}
