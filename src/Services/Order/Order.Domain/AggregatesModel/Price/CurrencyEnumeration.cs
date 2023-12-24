using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price
{
	public class CurrencyEnumeration : EnumerationBase
	{
		private CurrencyEnumeration(int id, string name) : base(id, name) { }

		/**
		 * ISO 4217 currency code.
		 */
		public static readonly CurrencyEnumeration EUR = new(1, nameof(EUR));
		public static readonly CurrencyEnumeration GBP = new(2, nameof(GBP));
	}
}
