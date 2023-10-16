using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Price.ValueObjects
{
	public class CurrencyValueObject : EnumerationBase
	{
		/**
		 * ISO 4217 currency code.
		 */
		public static readonly CurrencyValueObject EUR = new(1, nameof(EUR));
		public static readonly CurrencyValueObject GBP = new(2, nameof(GBP));

		private CurrencyValueObject(int id, string name) : base(id, name) { }
	}
}
