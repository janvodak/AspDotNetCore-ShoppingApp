namespace Order.Domain.Src.Price.ValueObjects
{
	public static class CurrencyExtensions
	{
		private static readonly Dictionary<CurrencyValueObject, CurrencyInformationValueObject> Currencies = new()
		{
			{
				CurrencyValueObject.EUR,
				new CurrencyInformationValueObject("Euro", "€", 978, 2, 100)
			},
			{
				CurrencyValueObject.GBP,
				new CurrencyInformationValueObject("Pound Sterling", "£", 826, 2, 100)
			}
		};

		public static int GetDefaultFractionDigits(this CurrencyValueObject currency)
		{
			return Currencies[currency].DefaultFractionDigits;
		}

		public static string GetDisplayName(this CurrencyValueObject currency)
		{
			return Currencies[currency].DisplayName;
		}

		public static string GetDisplaySymbol(this CurrencyValueObject currency)
		{
			return Currencies[currency].DisplaySymbol;
		}

		/**
		 * Returns the ISO 4217 numeric code of this currency.
		 */
		public static int GetNumericCode(this CurrencyValueObject currency)
		{
			return Currencies[currency].NumericCode;
		}

		public static int GetSubUnit(this CurrencyValueObject currency)
		{
			return Currencies[currency].SubUnit;
		}
	}
}
