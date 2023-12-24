namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price
{
	public static class CurrencyExtensions
	{
		private static readonly Dictionary<CurrencyEnumeration, CurrencyValueObject> Currencies = new()
		{
			{
				CurrencyEnumeration.EUR,
				new CurrencyValueObject("Euro", "€", 978, 2, 100)
			},
			{
				CurrencyEnumeration.GBP,
				new CurrencyValueObject("Pound Sterling", "£", 826, 2, 100)
			}
		};

		public static int GetDefaultFractionDigits(this CurrencyEnumeration currency)
		{
			return Currencies[currency].DefaultFractionDigits;
		}

		public static string GetDisplayName(this CurrencyEnumeration currency)
		{
			return Currencies[currency].DisplayName;
		}

		public static string GetDisplaySymbol(this CurrencyEnumeration currency)
		{
			return Currencies[currency].DisplaySymbol;
		}

		/**
		 * Returns the ISO 4217 numeric code of this currency.
		 */
		public static int GetNumericCode(this CurrencyEnumeration currency)
		{
			return Currencies[currency].NumericCode;
		}

		public static int GetSubUnit(this CurrencyEnumeration currency)
		{
			return Currencies[currency].SubUnit;
		}
	}
}
