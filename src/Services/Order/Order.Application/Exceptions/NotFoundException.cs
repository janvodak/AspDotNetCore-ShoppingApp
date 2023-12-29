namespace ShoppingApp.Services.Order.API.Application.Exceptions
{
	public class NotFoundException : AbstractException
	{
		public NotFoundException(string name, object key)
			: base($"Entity '{name}' ({key}) was not found.")
		{
		}
	}
}
