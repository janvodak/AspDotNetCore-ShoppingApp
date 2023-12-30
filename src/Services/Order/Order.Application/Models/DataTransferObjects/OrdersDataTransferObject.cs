namespace ShoppingApp.Services.Order.API.Application.Queries.GetOrdersList
{
	public record OrderDataTransferObject
	{
		public OrderDataTransferObject(
			int id,
			string userName,
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode,
			decimal totalPrice,
			int paymentMethod,
			string cardName,
			string cardNumber,
			string expiration,
			string cardVerificationValue)
		{
			Id = id;
			UserName = userName;
			FirstName = firstName;
			LastName = lastName;
			EmailAddress = emailAddress;
			AddressLine = addressLine;
			Country = country;
			State = state;
			ZipCode = zipCode;
			TotalPrice = totalPrice;
			PaymentMethod = paymentMethod;
			CardName = cardName;
			CardNumber = cardNumber;
			Expiration = expiration;
			CardVerificationValue = cardVerificationValue;
		}

		public int Id { get; init; }

		public string UserName { get; init; }

		public string FirstName { get; init; }

		public string LastName { get; init; }

		public string EmailAddress { get; init; }

		public string AddressLine { get; init; }

		public string Country { get; init; }

		public string State { get; init; }

		public string ZipCode { get; init; }

		public decimal TotalPrice { get; init; }

		public int PaymentMethod { get; init; }

		public string CardName { get; init; }

		public string CardNumber { get; init; }

		public string Expiration { get; init; }

		public string CardVerificationValue { get; init; }
	}
}
