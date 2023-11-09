using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Aggregator.Src.Models
{
	public class Order
	{
		public Order(
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

		public string UserName { get; set; }

		public decimal TotalPrice { get; set; }

		// BillingAddress
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string AddressLine { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }

		// Payment
		public string CardName { get; set; }
		public string CardNumber { get; set; }
		public string Expiration { get; set; }
		public string CardVerificationValue { get; set; }
		public int PaymentMethod { get; set; }
	}
}
