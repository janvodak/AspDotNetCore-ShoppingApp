using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Address.ValueObjects
{
	public class AddressValueObject : ValueObjectBase
	{
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string EmailAddress { get; private set; }
		public string AddressLine { get; private set; }
		public string Country { get; private set; }
		public string State { get; private set; }
		public string ZipCode { get; private set; }

		public AddressValueObject(
			string firstName,
			string lastName,
			string emailAddress,
			string addressLine,
			string country,
			string state,
			string zipCode)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
			this.EmailAddress = emailAddress;
			this.AddressLine = addressLine;
			this.Country = country;
			this.State = state;
			this.ZipCode = zipCode;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.FirstName;
			yield return this.LastName;
			yield return this.EmailAddress;
			yield return this.AddressLine;
			yield return this.Country;
			yield return this.State;
			yield return this.ZipCode;
		}
	}
}
