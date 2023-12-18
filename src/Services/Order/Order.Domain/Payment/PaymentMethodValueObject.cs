using ShoppingApp.Services.Order.API.Domain.Shared;

namespace ShoppingApp.Services.Order.API.Domain.Payment
{
	public class PaymentMethodValueObject : EnumerationBase
	{
		public static readonly PaymentMethodValueObject Card = new(1, nameof(Card));

		public PaymentMethodValueObject(int id, string name) : base(id, name) { }
	}
}