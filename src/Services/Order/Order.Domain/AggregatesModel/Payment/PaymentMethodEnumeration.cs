using ShoppingApp.Services.Order.API.Domain.Exceptions;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment
{
	public class PaymentMethodEnumeration : EnumerationBase
	{
		public PaymentMethodEnumeration(int id, string name) : base(id, name) { }

		public static readonly PaymentMethodEnumeration Card = new(1, nameof(Card));

		public static IEnumerable<PaymentMethodEnumeration> List()
		{
			return new[]
			{
				Card,
			};
		}

		public static PaymentMethodEnumeration FromName(string name)
		{
			var state = List()
				.SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

			if (state == null)
			{
				throw new DomainException($"Possible values for Payment method: {string.Join(",", List().Select(s => s.Name))}");
			}

			return state;
		}

		public static PaymentMethodEnumeration From(int id)
		{
			PaymentMethodEnumeration? state = List().SingleOrDefault(s => s.Id == id);

			if (state == null)
			{
				throw new DomainException($"Possible values for Payment method: {string.Join(",", List().Select(s => s.Name))}");
			}

			return state;
		}
	}
}
