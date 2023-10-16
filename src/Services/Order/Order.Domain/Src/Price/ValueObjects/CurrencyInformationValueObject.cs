using Order.Domain.Src.Shared;

namespace Order.Domain.Src.Price.ValueObjects
{
	public class CurrencyInformationValueObject : ValueObjectBase
	{
		public string DisplayName { get; private set; }
		public string DisplaySymbol { get; private set; }
		public int NumericCode { get; private set; }
		public int DefaultFractionDigits { get; private set; }
		public int SubUnit { get; private set; }

		public CurrencyInformationValueObject(
			string displayName,
			string displaySymbol,
			int numericCode,
			int defaultFractionDigits,
			int subUnit)
		{
			this.DisplayName = displayName;
			this.DisplaySymbol = displaySymbol;
			this.NumericCode = numericCode;
			this.DefaultFractionDigits = defaultFractionDigits;
			this.SubUnit = subUnit;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.DisplayName;
			yield return this.DisplaySymbol;
			yield return this.NumericCode;
			yield return this.DefaultFractionDigits;
			yield return this.SubUnit;
		}
	}
}
