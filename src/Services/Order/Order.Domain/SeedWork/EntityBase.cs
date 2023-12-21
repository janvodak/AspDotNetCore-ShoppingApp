using MediatR;

namespace ShoppingApp.Services.Order.API.Domain.SeedWork
{
	public abstract class EntityBase
	{
		private int? _requestedHashCode;
		private int _Id;
		private List<INotification> _domainEvents = null!;

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

		public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

		public string CreatedBy { get; set; } = null!;

		public DateTime CreatedDate { get; set; }

		public string? LastModifiedBy { get; set; } = null;

		public DateTime? LastModifiedDate { get; set; } = null;

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
