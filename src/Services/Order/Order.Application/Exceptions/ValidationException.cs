﻿using FluentValidation.Results;

namespace ShoppingApp.Services.Order.API.Application.Exceptions
{
	public class ValidationException : AbstractException
	{
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

		public IDictionary<string, string[]> Errors { get; }
	}
}
