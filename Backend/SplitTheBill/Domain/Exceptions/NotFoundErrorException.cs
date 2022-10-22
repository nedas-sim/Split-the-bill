namespace Domain.Exceptions;

public sealed class NotFoundErrorException : Exception
{
	public NotFoundErrorException(string message) : base(message)
	{
	}
}