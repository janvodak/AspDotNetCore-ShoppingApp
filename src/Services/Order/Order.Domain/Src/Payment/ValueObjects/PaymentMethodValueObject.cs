using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Payment.ValueObjects
{
	public class PaymentMethodValueObject : EnumerationBase
	{
		public static readonly PaymentMethodValueObject Card = new(1, nameof(Card));

		public PaymentMethodValueObject(int id, string name) : base(id, name) { }
	}
}