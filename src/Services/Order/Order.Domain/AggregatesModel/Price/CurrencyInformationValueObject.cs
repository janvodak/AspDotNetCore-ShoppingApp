using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price
{
	public class CurrencyInformationValueObject : ValueObjectBase
	{
		public CurrencyInformationValueObject(
			string displayName,
			string displaySymbol,
			int numericCode,
			int defaultFractionDigits,
			int subUnit)
		{
			DisplayName = displayName;
			DisplaySymbol = displaySymbol;
			NumericCode = numericCode;
			DefaultFractionDigits = defaultFractionDigits;
			SubUnit = subUnit;
		}

		public string DisplayName { get; private set; }
		public string DisplaySymbol { get; private set; }
		public int NumericCode { get; private set; }
		public int DefaultFractionDigits { get; private set; }
		public int SubUnit { get; private set; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return DisplayName;
			yield return DisplaySymbol;
			yield return NumericCode;
			yield return DefaultFractionDigits;
			yield return SubUnit;
		}
	}
}
