using Domain.Common;
using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseListRequest<TResponse> : PagingParameters, IRequest<BaseListResult<TResponse>>, IValidation
{
    public virtual string ApiErrorMessagePrefix() => string.Empty;

    public virtual IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        yield break;
    }
}