namespace ShoppingApp.Services.Order.API.Domain.Shared
{
	public abstract class EntityBase
	{
		public int Id { get; protected set; }

		public string CreatedBy { get; set; } = null!;

		public DateTime CreatedDate { get; set; }

		public string? LastModifiedBy { get; set; } = null;

		public DateTime? LastModifiedDate { get; set; } = null;
	}
}
