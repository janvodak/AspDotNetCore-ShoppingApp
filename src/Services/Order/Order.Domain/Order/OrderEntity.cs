using System.ComponentModel.DataAnnotations.Schema;
using ShoppingApp.Services.Order.API.Domain.Shared;

namespace ShoppingApp.Services.Order.API.Domain.Order
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
			UserName = userName;
			TotalPrice = totalPrice;
			FirstName = firstName;
			LastName = lastName;
			EmailAddress = emailAddress;
			AddressLine = addressLine;
			Country = country;
			State = state;
			ZipCode = zipCode;
			CardName = cardName;
			CardNumber = cardNumber;
			Expiration = expiration;
			CardVerificationValue = cardVerificationValue;
			PaymentMethod = paymentMethod;
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
