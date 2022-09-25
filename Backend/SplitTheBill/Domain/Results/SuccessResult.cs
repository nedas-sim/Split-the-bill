using Domain.Common.Results;

namespace Domain.Results;

public sealed class SuccessResult<T> : BaseResult<T> where T : class
{
    public T Item { get; set; }
}