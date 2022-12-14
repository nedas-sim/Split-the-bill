using Domain.Results;

namespace Domain.Common.Results;

public abstract class BaseResult<T>
{
    public static implicit operator BaseResult<T>(T item)
    {
        return new SuccessResult<T>
        {
            Item = item,
        };
    }
}