using Domain.Results;

namespace Domain.Extensions;

public static class ResultExtensions
{
    public static ListValidationResult<T> ToListValidationResult<T>(this string @this) => new() { Message = @this };
    public static ValidationErrorResult<T> ToValidationErrorResult<T>(this string @this) => new() { Message = @this };
    public static NotFoundResult<T> ToNotFoundResult<T>(this string @this) => new() { Message = @this };
}
