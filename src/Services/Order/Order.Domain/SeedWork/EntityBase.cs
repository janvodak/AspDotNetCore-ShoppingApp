using MediatR;

namespace ShoppingApp.Services.Order.API.Domain.SeedWork
{
	public abstract class EntityBase
	{
		private int _Id;

		private int? _requestedHashCode;

		private List<INotification>? _domainEvents;

		// DDD Patterns comment
		// Using private fields, allowed since EF Core 1.1, is a much better encapsulation
		// aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
		protected string _createdBy = null!;

		protected DateTime _createdDate;

		protected string? _lastModifiedBy = null;

		protected DateTime? _lastModifiedDate = null;

		public virtual int Id
		{
			get
			{
				return _Id;
			}

			protected set
			{
				_Id = value;
			}
		}

		public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

		public void AddDomainEvent(INotification eventItem)
		{
			_domainEvents ??= new List<INotification>();

			_domainEvents.Add(eventItem);
		}

		public void RemoveDomainEvent(INotification eventItem)
		{
			_domainEvents?.Remove(eventItem);
		}

		public void ClearDomainEvents()
		{
			_domainEvents?.Clear();
		}

		public void SetCreatedBy(string name)
		{
			_createdBy = name;
		}

		public void SetCreatedDate(DateTime dateTime)
		{
			_createdDate = dateTime;
		}

		public void SetLastModifiedBy(string name)
		{
			_lastModifiedBy = name;
		}

		public void SetLastModifiedDate(DateTime dateTime)
		{
			_lastModifiedDate = dateTime;
		}

		public bool IsTransient()
		{
			return this.Id == default;
		}

		public override bool Equals(object? obj = null)
		{
			if (obj == null || obj is not EntityBase)
			{
				return false;
			}
			else if (Object.ReferenceEquals(this, obj) == true)
			{
				return true;
			}
			else if (this.GetType() != obj.GetType())
			{
				return false;
			}

			EntityBase item = (EntityBase)obj;

			if (item.IsTransient() == true || this.IsTransient() == true)
			{
				return false;
			}

			return item.Id == this.Id;
		}

		public override int GetHashCode()
		{
			if (IsTransient() == true)
			{
				return base.GetHashCode();
			}

			if (_requestedHashCode.HasValue == false)
			{
				_requestedHashCode = this.Id.GetHashCode() ^ 31;
			}

			// XOR for random distribution. See:
			// https://learn.microsoft.com/archive/blogs/ericlippert/guidelines-and-rules-for-gethashcode
			return _requestedHashCode.Value;
				
		}

		public static bool operator ==(EntityBase left, EntityBase right)
		{
			if (Object.Equals(left, null))
			{
				return (Object.Equals(right, null));
			}

			return left.Equals(right);
		}

		public static bool operator !=(EntityBase left, EntityBase right)
		{
			return left != right;
		}
	}
}
