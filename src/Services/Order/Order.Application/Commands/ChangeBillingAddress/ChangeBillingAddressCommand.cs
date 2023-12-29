using System.Runtime.Serialization;
using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Commands.UpdateOrder
{
	public class ChangeBillingAddressCommand : IRequest<bool>
	{
		[DataMember]
		public int Id { get; private set; }

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

		public ChangeBillingAddressCommand(
			int id,
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			EmailAddress = emailAddress;
			AddressLine = addressLine;
			Country = country;
			State = state;
			ZipCode = zipCode;
		}
	}
}
