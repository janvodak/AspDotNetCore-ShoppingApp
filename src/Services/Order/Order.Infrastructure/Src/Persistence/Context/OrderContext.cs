using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Order.Domain.Src.Order.Entities;
using Order.Domain.Src.Shared;

namespace Order.Infrastructure.Src.Persistence.Context
{
	public class OrderContext : DbContext
	{
		private const string AUTOMAT_NAME = "swn";

		public virtual DbSet<OrderEntity> Orders { get; set; } = null!;

		private readonly string _connectionString;

		public OrderContext(IOptions<DatabaseSettings> databaseSettings)
		{
			this._connectionString = String.Format(
				format: databaseSettings.Value.ConnectionStringTemplate,
				databaseSettings.Value.Server,
				databaseSettings.Value.DBname,
				databaseSettings.Value.User,
				databaseSettings.Value.Password);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<EntityBase>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.Now;
						entry.Entity.CreatedBy = OrderContext.AUTOMAT_NAME;
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedDate = DateTime.Now;
						entry.Entity.LastModifiedBy = OrderContext.AUTOMAT_NAME;
						break;
					default:
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseSqlServer(this._connectionString);
		}
	}
}
