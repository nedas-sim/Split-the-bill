using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.Common;

public abstract class BaseValidation
{
    public abstract string ApiErrorMessagePrefix { get; }

    public virtual IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        yield break;
    }

    public bool IsValid(out string? errorMessage)
    {
        List<string> validations =
            ValidateProperties()
                .Where(v => v.Success is false)
                .Select(v => v.ErrorMessage)
                .ToList();

        if (validations.Any() is false)
        {
            errorMessage = null;
            return true;
        }

        errorMessage = validations.BuildErrorMessage(ApiErrorMessagePrefix);

        return string.IsNullOrEmpty(errorMessage);
    }

    public void ValidateAndThrow()
    {
        if (IsValid(out string? errorMessage) is false)
        {
            throw new ValidationErrorException(errorMessage!);
        }
    }
}
