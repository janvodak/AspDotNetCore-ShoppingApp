using System.Runtime.Serialization;
using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Commands.UpdateOrder
{
	[DataContract]
	public class UpdateOrderCommand : IRequest<bool>
	{
		[DataMember]
		public int Id { get; private set; }

		[DataMember]
		public string UserName { get; private set; }

		[DataMember]
		public string FirstName { get; private set; }

		[DataMember]
		public string LastName { get; private set; }

		[DataMember]
		public string EmailAddress { get; private set; }

		[DataMember]
		public string AddressLine { get; private set; }

		[DataMember]
		public string Country { get; private set; }

		[DataMember]
		public string State { get; private set; }

		[DataMember]
		public string ZipCode { get; private set; }

		[DataMember]
		public decimal TotalPrice { get; private set; }

		[DataMember]
		public int PaymentMethod { get; private set; }

		[DataMember]
		public string CardName { get; private set; }

		[DataMember]
		public string CardNumber { get; private set; }

		[DataMember]
		public string Expiration { get; private set; }

		[DataMember]
		public string CardVerificationValue { get; private set; }

		public UpdateOrderCommand(
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
	}
}
