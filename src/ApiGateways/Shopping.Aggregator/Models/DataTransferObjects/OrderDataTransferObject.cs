﻿namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects
{
	public class OrderDataTransferObject
	{
		public OrderDataTransferObject(
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
