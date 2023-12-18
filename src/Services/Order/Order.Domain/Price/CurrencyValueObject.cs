using ShoppingApp.Services.Order.API.Domain.Shared;

namespace ShoppingApp.Services.Order.API.Domain.Price
{
	public class CurrencyValueObject : EnumerationBase
	{
		private CurrencyValueObject(int id, string name) : base(id, name) { }

		/**
		 * ISO 4217 currency code.
		 */
		public static readonly CurrencyValueObject EUR = new(1, nameof(EUR));
		public static readonly CurrencyValueObject GBP = new(2, nameof(GBP));
	}
}
