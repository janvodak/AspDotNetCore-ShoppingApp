using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Discount.Grpc.Configuration.DataTransferObjects;
using ShoppingApp.Services.Discount.Grpc.Models;

namespace ShoppingApp.Services.Discount.Grpc.Data
{
	public partial class DiscountContext : DbContext
	{
		private readonly IOptions<DatabaseSettings> _databaseSettings;

		public DiscountContext(IOptions<DatabaseSettings> databaseSettings)
		{
			_databaseSettings = databaseSettings;
		}

		public virtual DbSet<DiscountModel> Discounts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseNpgsql(_databaseSettings.Value.GetConnectionString());
		}
	}
}
