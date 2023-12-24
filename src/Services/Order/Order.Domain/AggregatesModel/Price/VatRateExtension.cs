namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price
{
	public static class VatRateExtensions
	{
		private static readonly Dictionary<VatRateEnumeration, VatRateValueObject> VatRates = new()
		{
			{
				VatRateEnumeration.DEFAULT,
				new VatRateValueObject(20)
			}
		};

		public static decimal GetVatCoefficient(this VatRateEnumeration vatRate)
		{
			return VatRates[vatRate].GetVatCoefficient();
		}
	}
}
