namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects.Factories
{
	public interface IOrderFactory
	{
		Task<IEnumerable<OrderDataTransferObject>> Create(string userName);
	}
}