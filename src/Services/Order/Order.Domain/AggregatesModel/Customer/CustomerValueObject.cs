using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Customer
{
	public class CustomerValueObject : ValueObjectBase
	{
		public CustomerValueObject(string userName)
		{
			UserName = userName;
		}

		public string UserName { get; private set; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return UserName;
		}
	}
}
