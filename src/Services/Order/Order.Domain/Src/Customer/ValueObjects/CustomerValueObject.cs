using System;
using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Customer.ValueObjects
{
	public class CustomerValueObject : ValueObjectBase
	{
		public string UserName { get; set; }

		public CustomerValueObject(string userName)
		{
			this.UserName = userName;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.UserName;
		}
	}
}
