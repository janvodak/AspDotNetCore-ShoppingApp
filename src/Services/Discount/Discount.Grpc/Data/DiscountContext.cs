using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Discount.Grpc.Models;

namespace ShoppingApp.Services.Discount.Grpc.Data
{
	public partial class DiscountContext : DbContext
	{
		public virtual DbSet<DiscountModel> Discounts { get; set; } = null!;

		private readonly string _connectionString;

		public DiscountContext(IOptions<DatabaseSettings> databaseSettings)
		{
			_connectionString = string.Format(
				"User ID={0};Password={1};Host={2};Port={3};Database={4};",
				databaseSettings.Value.User,
				databaseSettings.Value.Password,
				databaseSettings.Value.Host,
				databaseSettings.Value.Port,
				databaseSettings.Value.DBname);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseNpgsql(_connectionString);
		}
	}
}
