namespace Domain.Common.Results;

public abstract class BaseMessageResult<T> : BaseResult<T> //where T : class
{
    public string Message { get; set; } = default!;
}