using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price
{
	public class VatRateEnumeration : EnumerationBase
	{
		private VatRateEnumeration(int id, string name) : base(id, name) { }

		public static readonly VatRateEnumeration DEFAULT = new(1, nameof(DEFAULT));
	}
}
