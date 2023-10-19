using Order.Domain.Src.Address.ValueObjects;
using Order.Domain.Src.Customer.ValueObjects;
using Order.Domain.Src.Payment.ValueObjects;
using Order.Domain.Src.Price.ValueObjects;
using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Order.Entities
{
	public class OrderEntity : EntityBase
	{
		public string UserName { get; set; } = null!;
		public decimal TotalPrice { get; set; }

		// BillingAddress
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string EmailAddress { get; set; } = null!;
		public string AddressLine { get; set; } = null!;
		public string Country { get; set; } = null!;
		public string State { get; set; } = null!;
		public string ZipCode { get; set; } = null!;

		// Payment
		public string CardName { get; set; } = null!;
		public string CardNumber { get; set; } = null!;
		public string Expiration { get; set; } = null!;
		public string CVV { get; set; } = null!;
		public int PaymentMethod { get; set; }

		//public CustomerValueObject Customer { get; set; }
		//public PriceValueObject TotalPrice { get; set; }
		//public AddressValueObject BillingAddress { get; set; }
		//public PaymentCardValueObject PaymentCard { get; set; }
		//public PaymentMethodValueObject PaymentMethod { get; set; }

		//public OrderEntity(
		//	CustomerValueObject customer,
		//	PriceValueObject totalPrice,
		//	AddressValueObject billingAddress,
		//	PaymentCardValueObject paymentCard,
		//	PaymentMethodValueObject paymentMethod)
		//{
		//	this.Customer = customer;
		//	this.TotalPrice = totalPrice;
		//	this.BillingAddress = billingAddress;
		//	this.PaymentCard = paymentCard;
		//	this.PaymentMethod = paymentMethod;
		//}
	}
}
