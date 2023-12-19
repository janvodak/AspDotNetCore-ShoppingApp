using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment
{
	public class PaymentMethodValueObject : EnumerationBase
	{
		public static readonly PaymentMethodValueObject Card = new(1, nameof(Card));

		public PaymentMethodValueObject(int id, string name) : base(id, name) { }
	}
}