using Order.Domain.Src.Address.ValueObjects;
using Order.Domain.Src.Customer.ValueObjects;
using Order.Domain.Src.Payment.ValueObjects;
using Order.Domain.Src.Price.ValueObjects;
using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Order.Entities
{
	public class OrderEntity : EntityBase
	{
		public CustomerValueObject Customer { get; set; }
		public PriceValueObject TotalPrice { get; set; }
		public AddressValueObject BillingAddress { get; set; }
		public PaymentCardValueObject PaymentCard { get; set; }
		public PaymentMethodValueObject PaymentMethod { get; set; }

		public OrderEntity(
			CustomerValueObject customer,
			PriceValueObject totalPrice,
			AddressValueObject billingAddress,
			PaymentCardValueObject paymentCard,
			PaymentMethodValueObject paymentMethod)
		{
			this.Customer = customer;
			this.TotalPrice = totalPrice;
			this.BillingAddress = billingAddress;
			this.PaymentCard = paymentCard;
			this.PaymentMethod = paymentMethod;
		}
	}
}
