using System.ComponentModel.DataAnnotations;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Address;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Customer;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities
{
	public class OrderAggregateRoot : EntityBase, IAggregateRoot
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public OrderAggregateRoot(){ }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		public OrderAggregateRoot(
			CustomerValueObject customer,
			PriceValueObject totalPrice,
			AddressValueObject billingAddress,
			PaymentCardValueObject paymentCard,
			PaymentMethodEnumeration paymentMethod)
		{
			Customer = customer;
			TotalPrice = totalPrice;
			BillingAddress = billingAddress;
			PaymentCard = paymentCard;
			PaymentMethod = paymentMethod;
		}

		[Required]
		public CustomerValueObject Customer { get; private set; }

		[Required]
		public PriceValueObject TotalPrice { get; private set; }

		[Required]
		public AddressValueObject BillingAddress { get; private set; }

		[Required]
		public PaymentCardValueObject PaymentCard { get; private set; }

		[Required]
		public PaymentMethodEnumeration PaymentMethod { get; private set; }
	}
}
