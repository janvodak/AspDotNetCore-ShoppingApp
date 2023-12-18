using ShoppingApp.Services.Order.API.Domain.Shared;

namespace ShoppingApp.Services.Order.API.Domain.Customer
{
	public class CustomerValueObject : ValueObjectBase
	{
		public CustomerValueObject(string userName)
		{
			UserName = userName;
		}

		public string UserName { get; set; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return UserName;
		}
	}
}
