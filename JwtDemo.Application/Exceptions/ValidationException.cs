using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtDemo.Application.Exceptions
{
   public sealed class ValidationException : Exception
{
	public ValidationException(IEnumerable<ValidationError> errors)
	{
		Errors = errors;
	}

	public IEnumerable<ValidationError> Errors { get; }
}
}