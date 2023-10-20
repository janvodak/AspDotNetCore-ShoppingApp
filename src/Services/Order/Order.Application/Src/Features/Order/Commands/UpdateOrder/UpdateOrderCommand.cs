using MediatR;

namespace Order.Application.Src.Features.Order.Commands.UpdateOrder
{
	public class UpdateOrderCommand : IRequest
	{
		public int Id { get; set; }

		public string UserName { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string EmailAddress { get; set; } = null!;
		public string AddressLine { get; set; } = null!;
		public string Country { get; set; } = null!;
		public string State { get; set; } = null!;
		public string ZipCode { get; set; } = null!;

		public decimal TotalPrice { get; set; }

		public int PaymentMethod { get; set; }

		public string CardName { get; set; } = null!;
		public string CardNumber { get; set; } = null!;
		public string Expiration { get; set; } = null!;
		public string CardVerificationValue { get; set; } = null!;
	}
}
