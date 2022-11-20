using Domain.Common.Results;

namespace Domain.Results;

public sealed class SuccessResult<T> : BaseResult<T>
{
    public T Item { get; set; }
}