namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects.Factories
{
	public interface IProductFactory
	{
		Task<ProductDataTransferObject?> Create(string id);
	}
}