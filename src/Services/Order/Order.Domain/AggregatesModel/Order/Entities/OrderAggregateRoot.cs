using System.ComponentModel.DataAnnotations;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities
{
	public class OrderAggregateRoot : EntityBase, IAggregateRoot
	{
		// This empty constructor fix issue with mapped properties in Infrastructure layer EF migration table mappings
		// No suitable constructor was found for entity type 'OrderAggregateRoot'.
		// Note that only mapped properties can be bound to constructor parameters.
		// Navigations to related entities, including references to owned types, cannot be bound.
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
