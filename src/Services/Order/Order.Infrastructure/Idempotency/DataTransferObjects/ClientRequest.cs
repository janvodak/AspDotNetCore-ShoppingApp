using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Services.Order.API.Infrastructure.Idempotency.DataTransferObjects
{
	public class ClientRequest
	{
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		public DateTime Time { get; set; }
	}
}
