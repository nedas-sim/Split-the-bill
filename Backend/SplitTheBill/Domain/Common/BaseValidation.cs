using Domain.Exceptions;

namespace Domain.Common;

public abstract class BaseValidation
{
    public virtual bool IsValid(out string? errorMessage)
    {
        // By default the request is valid
        errorMessage = null;
        return true;
    }

    public void ValidateAndThrow()
    {
        if (IsValid(out string? errorMessage) is false)
        {
            throw new ValidationErrorException(errorMessage!);
        }
    }
}
