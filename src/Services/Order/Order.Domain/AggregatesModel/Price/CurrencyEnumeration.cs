using ShoppingApp.Services.Order.API.Domain.Exceptions;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Price
{
	public class CurrencyEnumeration : EnumerationBase
	{
		private readonly Dictionary<string, Dictionary<string, object>> CURRENCIES = new()
		{
			["EUR"] = new Dictionary<string, object>
			{
				["display_name"] = "Euro",
				["display_symbol"] = "€",
				["numeric_code"] = 978,
				["default_fraction_digits"] = 2,
				["sub_unit"] = 100,
			},
			["GBP"] = new Dictionary<string, object>
			{
				["display_name"] = "Pound Sterling",
				["display_symbol"] = "£",
				["numeric_code"] = 826,
				["default_fraction_digits"] = 2,
				["sub_unit"] = 100,
			},
		};

		public CurrencyEnumeration(int id, string name) : base(id, name) { }

		/**
		 * ISO 4217 currency code.
		 */
		public static readonly CurrencyEnumeration EUR = new(1, nameof(EUR));
		public static readonly CurrencyEnumeration GBP = new(2, nameof(GBP));

		public static IEnumerable<CurrencyEnumeration> List()
		{
			return new[]
			{
				EUR,
				GBP,
			};
		}

		public static CurrencyEnumeration FromName(string name)
		{
			var state = List()
				.SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

			if (state == null)
			{
				throw new DomainException($"Possible values for Currency: {string.Join(",", List().Select(s => s.Name))}");
			}

			return state;
		}

		public static CurrencyEnumeration From(int id)
		{
			CurrencyEnumeration? state = List().SingleOrDefault(s => s.Id == id);

			if (state == null)
			{
				throw new DomainException($"Possible values for Currency: {string.Join(",", List().Select(s => s.Name))}");
			}

			return state;
		}

		public int GetDefaultFractionDigits()
		{
			return (int)CURRENCIES[Name]["default_fraction_digits"];
		}

		public string GetDisplayName()
		{
			return (string)CURRENCIES[Name]["display_name"];
		}

		public string GetDisplaySymbol()
		{
			return (string)CURRENCIES[Name]["display_symbol"];
		}

		/**
		 * Returns the ISO 4217 numeric code of this currency.
		 */
		public int GetNumericCode()
		{
			return (int)CURRENCIES[Name]["numeric_code"];
		}

		public int GetSubUnit()
		{
			return (int)CURRENCIES[Name]["sub_unit"];
		}
	}
}
