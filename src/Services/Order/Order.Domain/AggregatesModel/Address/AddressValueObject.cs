using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Address
{
	public class AddressValueObject : ValueObjectBase
	{
		public AddressValueObject(
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode)
		{
			FirstName = firstName;
			LastName = lastName;
			EmailAddress = emailAddress;
			AddressLine = addressLine;
			Country = country;
			State = state;
			ZipCode = zipCode;
		}

		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string EmailAddress { get; private set; }
		public string AddressLine { get; private set; }
		public string Country { get; private set; }
		public string State { get; private set; }
		public string ZipCode { get; private set; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return FirstName;
			yield return LastName;
			yield return EmailAddress;
			yield return AddressLine;
			yield return Country;
			yield return State;
			yield return ZipCode;
		}
	}
}
