using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.SeedWork;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.EntityConfiguration;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Extensions;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Policies;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context
{
	public class OrderContext : DbContext, IUnitOfWork
	{
		private const string AUTOMAT_NAME = "swn";

		private readonly DatabaseSettings _databaseSettings;

		private readonly EntityFrameworkPolicySettings _entityFrameworkPolicySettings;

		private readonly IMediator _mediator;

		private IDbContextTransaction? _currentTransaction = null!;

		public OrderContext(
			IOptions<DatabaseSettings> databaseSettings,
			IOptions<EntityFrameworkPolicySettings> entityFrameworkPolicySettings,
			IMediator mediator)
		{
			_databaseSettings = databaseSettings.Value;
			_entityFrameworkPolicySettings = entityFrameworkPolicySettings.Value;
			_mediator = mediator;
		}

		public bool HasActiveTransaction => _currentTransaction != null;

		public virtual DbSet<OrderAggregateRoot> Orders { get; set; } = null!;

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
		{
			// Dispatch Domain Events collection. 
			// Choices:
			// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
			// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
			// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
			// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
			await _mediator.DispatchDomainEventsAsync(this);

			// After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
			// performed through the DbContext will be committed
			_ = await SaveChangesAsync(cancellationToken);

			return true;
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<EntityBase>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.SetCreatedDate(DateTime.Now);
						entry.Entity.SetCreatedBy(AUTOMAT_NAME);
						break;
					case EntityState.Modified:
						entry.Entity.SetLastModifiedDate(DateTime.Now);
						entry.Entity.SetLastModifiedBy(AUTOMAT_NAME);
						break;
					default:
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		public async Task<IDbContextTransaction?> BeginTransactionAsync()
		{
			if (_currentTransaction != null)
			{
				return null;
			}

			_currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

			return _currentTransaction;
		}

		public async Task CommitTransactionAsync(IDbContextTransaction transaction)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException(nameof(transaction));
			}

			if (transaction != _currentTransaction)
			{
				throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
			}

			try
			{
				await SaveChangesAsync();
				await transaction.CommitAsync();
			}
			catch
			{
				RollbackTransaction();
				throw;
			}

			finally
			{
				if (_currentTransaction != null)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}
			}
		}

		public void RollbackTransaction()
		{
			try
			{
				_currentTransaction?.Rollback();
			}
			finally
			{
				if (_currentTransaction != null)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}
			}
		}

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseSqlServer(
				_databaseSettings.GetConnectionString(),
				sqlServerOptionsAction: sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(
						maxRetryCount: _entityFrameworkPolicySettings.MaxRetryCount,
						maxRetryDelay: TimeSpan.FromSeconds(_entityFrameworkPolicySettings.MaxRetryDelay),
						errorNumbersToAdd: null);
				});
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("OrderSchema");

			modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
		}
	}
}
