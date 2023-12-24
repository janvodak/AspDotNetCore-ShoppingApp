using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment
{
	public class PaymentMethodEnumeration : EnumerationBase
	{
		public static readonly PaymentMethodEnumeration Card = new(1, nameof(Card));

		public PaymentMethodEnumeration(int id, string name) : base(id, name) { }
	}
}