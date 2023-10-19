using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Order.Application.Src.Models;
using Order.Domain.Src.Order.Entities;
using Order.Domain.Src.Shared;

namespace Order.Infrastructure.Src.Persistence.Context
{
	public class OrderContext : DbContext
	{
		public virtual DbSet<OrderEntity> Orders { get; set; } = null!;

		private readonly string _connectionString;

		public OrderContext(IOptions<DatabaseSettings> databaseSettings)
		{
			this._connectionString = String.Format(
				"User ID={0};Password={1};Host={2};Database={3};",
				databaseSettings.Value.User,
				databaseSettings.Value.Password,
				databaseSettings.Value.Host,
				databaseSettings.Value.DBname);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<EntityBase>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.Now;
						entry.Entity.CreatedBy = "swn";
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedDate = DateTime.Now;
						entry.Entity.LastModifiedBy = "swn";
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
