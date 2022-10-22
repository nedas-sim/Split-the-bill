namespace Domain.Exceptions;

public sealed class ValidationErrorException : Exception
{
	public ValidationErrorException(string message) : base(message)
	{
	}
}