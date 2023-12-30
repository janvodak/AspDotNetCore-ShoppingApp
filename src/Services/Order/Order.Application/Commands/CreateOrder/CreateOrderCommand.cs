using System.Runtime.Serialization;
using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Commands.CreateOrder
{
	// DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
	// In this case, its immutability is achieved by having all the setters as private
	// plus only being able to update the data just once, when creating the object through its constructor.
	// References on Immutable Commands:  
	// http://cqrs.nu/Faq
	// https://docs.spine3.org/motivation/immutability.html 
	// http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
	// https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-a-lightweight-class-with-auto-implemented-properties

	[DataContract]
	public class CreateOrderCommand : IRequest<bool>
	{
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

		public CreateOrderCommand(
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
