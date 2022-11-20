namespace Domain.Common.Results;

public abstract class BaseMessageResult<T> : BaseResult<T>
{
    public string Message { get; set; } = default!;
}