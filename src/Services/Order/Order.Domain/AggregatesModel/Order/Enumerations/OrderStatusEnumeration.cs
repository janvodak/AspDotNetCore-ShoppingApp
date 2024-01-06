using ShoppingApp.Services.Order.API.Domain.Exceptions;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Enumerations
{
	public class OrderStatusEnumeration : EnumerationBase
	{
		public OrderStatusEnumeration(int id, string name) : base(id, name) { }

		public static readonly OrderStatusEnumeration Submitted = new(1, nameof(Submitted).ToLowerInvariant());
		public static readonly OrderStatusEnumeration AwaitingValidation = new(2, nameof(AwaitingValidation).ToLowerInvariant());
		public static readonly OrderStatusEnumeration StockConfirmed = new(3, nameof(StockConfirmed).ToLowerInvariant());
		public static readonly OrderStatusEnumeration Paid = new(4, nameof(Paid).ToLowerInvariant());
		public static readonly OrderStatusEnumeration Shipped = new(5, nameof(Shipped).ToLowerInvariant());
		public static readonly OrderStatusEnumeration Cancelled = new(6, nameof(Cancelled).ToLowerInvariant());

		public static IEnumerable<OrderStatusEnumeration> List()
		{
			return new[]
			{
				Submitted,
				AwaitingValidation,
				StockConfirmed,
				Paid,
				Shipped,
				Cancelled
			};
		}

		public static OrderStatusEnumeration FromName(string name)
		{
			var state = List()
				.SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

			if (state == null)
			{
				throw new DomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
			}

			return state;
		}

		public static OrderStatusEnumeration From(int id)
		{
			OrderStatusEnumeration? state = List().SingleOrDefault(s => s.Id == id);

			if (state == null)
			{
				throw new DomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
			}

			return state;
		}
	}
}
