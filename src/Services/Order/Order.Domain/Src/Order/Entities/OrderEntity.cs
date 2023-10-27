using System.ComponentModel.DataAnnotations.Schema;
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

		[Column(TypeName = "decimal(6, 2)")]
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
		public string CardVerificationValue { get; set; } = null!;
		public int PaymentMethod { get; set; }

		public OrderEntity(
			string userName,
			decimal totalPrice,
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode,
			string cardName,
			string cardNumber,
			string expiration,
			string cardVerificationValue,
			int paymentMethod)
		{
			this.UserName = userName;
			this.TotalPrice = totalPrice;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.EmailAddress = emailAddress;
			this.AddressLine = addressLine;
			this.Country = country;
			this.State = state;
			this.ZipCode = zipCode;
			this.CardName = cardName;
			this.CardNumber = cardNumber;
			this.Expiration = expiration;
			this.CardVerificationValue = cardVerificationValue;
			this.PaymentMethod = paymentMethod;
		}

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
