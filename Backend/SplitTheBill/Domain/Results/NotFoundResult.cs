using Domain.Common.Results;

namespace Domain.Results;

public sealed class NotFoundResult<T> : BaseMessageResult<T> //where T : class
{
}