using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Payment;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.EntityConfiguration
{
	class OrderEntityTypeConfiguration : IEntityTypeConfiguration<OrderAggregateRoot>
	{
		public void Configure(EntityTypeBuilder<OrderAggregateRoot> orderConfiguration)
		{
			orderConfiguration.ToTable("Orders");

			orderConfiguration.Ignore(o => o.DomainEvents);

			orderConfiguration.Property(o => o.Id)
				.UseHiLo("orderseq");

			//Customer value object persisted as owned entity type supported since EF Core 2.0
			orderConfiguration
				.OwnsOne(o => o.Customer, customer =>
				{
					customer.Property(c => c.UserName).HasColumnName("UserName");
				});

			orderConfiguration
				.Property(o => o.TotalPrice)
				.HasColumnName("TotalPrice")
				.HasColumnType("decimal(6,2)")
				.HasConversion(
					v => v.MoneyWithVat.Amount + v.GetVatAmount(),
					v => PriceValueObject.FromFloat(v, CurrencyEnumeration.EUR, new VatRateValueObject(20))
				);

			//BillingAddress value object persisted as owned entity type supported since EF Core 2.0
			orderConfiguration
				.OwnsOne(o => o.BillingAddress, address =>
				{
					address.Property(a => a.FirstName).HasColumnName("FirstName");
					address.Property(a => a.LastName).HasColumnName("LastName");
					address.Property(a => a.EmailAddress).HasColumnName("EmailAddress");
					address.Property(a => a.AddressLine).HasColumnName("AddressLine");
					address.Property(a => a.Country).HasColumnName("Country");
					address.Property(a => a.State).HasColumnName("State");
					address.Property(a => a.ZipCode).HasColumnName("ZipCode");
				});

			//PaymentCard value object persisted as owned entity type supported since EF Core 2.0
			orderConfiguration
				.OwnsOne(o => o.PaymentCard, card =>
				{
					card.Property(c => c.CardName).HasColumnName("CardName");
					card.Property(c => c.CardNumber).HasColumnName("CardNumber");
					card.Property(c => c.Expiration).HasColumnName("Expiration");
					card.Property(c => c.CardVerificationValue).HasColumnName("CardVerificationValue");
				});

			orderConfiguration
				.Property(o => o.PaymentMethod)
				.HasColumnName("PaymentMethod")
				.HasColumnType("int")
				.HasConversion(
					v => v.Id,
					v => new PaymentMethodEnumeration(v, PaymentMethodEnumeration.Card.Name)
				);

			orderConfiguration
				.Property("_createdBy")
				.HasColumnName("CreatedBy");

			orderConfiguration
				.Property("_createdDate")
				.HasColumnName("CreatedDate");

			orderConfiguration
				.Property("_lastModifiedBy")
				.HasColumnName("LastModifiedBy");

			orderConfiguration
				.Property("_lastModifiedDate")
				.HasColumnName("LastModifiedDate");
		}
	}
}
