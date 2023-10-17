using FluentValidation.Results;

namespace Order.Application.Src.Exceptions
{
	public class ValidationException : ApplicationException
	{
		public IDictionary<string, string[]> Errors { get; }

		public ValidationException()
			: base("Orne or more validation failures have occured.")
		{
			Errors = new Dictionary<string, string[]>();
		}

		public ValidationException(IEnumerable<ValidationFailure> failures)
			: this()
		{
			Errors = failures
				.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
				.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
		}
	}
}
