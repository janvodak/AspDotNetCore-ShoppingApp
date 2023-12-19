using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context
{
	public class OrderContext : DbContext
	{
		private const string AUTOMAT_NAME = "swn";

		public virtual DbSet<OrderAggregateRoot> Orders { get; set; } = null!;

		private readonly string _connectionString;

		public OrderContext(IOptions<DatabaseSettings> databaseSettings)
		{
			_connectionString = string.Format(
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
						entry.Entity.CreatedBy = AUTOMAT_NAME;
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedDate = DateTime.Now;
						entry.Entity.LastModifiedBy = AUTOMAT_NAME;
						break;
					default:
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseSqlServer(_connectionString);
		}
	}
}
