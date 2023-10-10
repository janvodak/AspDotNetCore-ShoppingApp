using Discount.Grpc.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Discount.Grpc.Src.Data
{
	public partial class DiscountContext : DbContext
	{
		public virtual DbSet<DiscountEntity> Discounts { get; set; } = null!;

		private readonly string _connectionString;

		public DiscountContext(IOptions<DatabaseSettings> databaseSettings)
		{
			this._connectionString = String.Format(
				"User ID={0};Password={1};Host={2};Port={3};Database={4};",
				databaseSettings.Value.User,
				databaseSettings.Value.Password,
				databaseSettings.Value.Host,
				databaseSettings.Value.Port,
				databaseSettings.Value.DBname);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseNpgsql(this._connectionString);
		}
	}
}
