namespace ShoppingApp.Services.Order.API.Infrastructure.Exceptions
{
	/// <summary>
	/// Exception type for application exceptions
	/// </summary>
	public abstract class AbstractException : Exception
	{
		public AbstractException()
		{ }

		public AbstractException(string message)
			: base(message)
		{ }

		public AbstractException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
