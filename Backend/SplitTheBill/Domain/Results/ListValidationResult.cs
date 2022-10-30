using Domain.Common.Results;

namespace Domain.Results;

public sealed class ListValidationResult<T> : BaseListResult<T>
{
    public string Message { get; set; }
}