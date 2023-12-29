using ShoppingApp.Services.Order.API.Domain.Exceptions;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Enumerations
{
	public class OrderStatusEnumeration : EnumerationBase
	{
		public static OrderStatusEnumeration Submitted = new OrderStatusEnumeration(1, nameof(Submitted).ToLowerInvariant());
		public static OrderStatusEnumeration AwaitingValidation = new OrderStatusEnumeration(2, nameof(AwaitingValidation).ToLowerInvariant());
		public static OrderStatusEnumeration StockConfirmed = new OrderStatusEnumeration(3, nameof(StockConfirmed).ToLowerInvariant());
		public static OrderStatusEnumeration Paid = new OrderStatusEnumeration(4, nameof(Paid).ToLowerInvariant());
		public static OrderStatusEnumeration Shipped = new OrderStatusEnumeration(5, nameof(Shipped).ToLowerInvariant());
		public static OrderStatusEnumeration Cancelled = new OrderStatusEnumeration(6, nameof(Cancelled).ToLowerInvariant());

		public OrderStatusEnumeration(int id, string name) : base(id, name) { }

		public static IEnumerable<OrderStatusEnumeration> List() =>
			new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };

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
			var state = List().SingleOrDefault(s => s.Id == id);

			if (state == null)
			{
				throw new DomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
			}

			return state;
		}
	}
}
