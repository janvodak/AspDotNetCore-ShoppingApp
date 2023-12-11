using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers
{
	public interface IOrderHandler
	{
		Task<IEnumerable<OrderDataTransferObject>> Handle(string userName);
	}
}