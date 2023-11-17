using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Discount.API.Models;

namespace ShoppingApp.Services.Discount.API.Data
{
	public partial class DiscountContext : DbContext
	{
		private readonly string _connectionString;

		public DiscountContext(IOptions<DatabaseSettings> databaseSettings)
		{
			_connectionString = string.Format(
				databaseSettings.Value.ConnectionStringTemplate,
				databaseSettings.Value.User,
				databaseSettings.Value.Password,
				databaseSettings.Value.Host,
				databaseSettings.Value.Port,
				databaseSettings.Value.DBname);
		}

		public virtual DbSet<DiscountDataTransferObject> Discounts { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseNpgsql(_connectionString);
		}
	}
}
