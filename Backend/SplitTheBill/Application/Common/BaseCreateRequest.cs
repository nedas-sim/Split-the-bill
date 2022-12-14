using Domain.Common;
using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Responses;
using MediatR;

namespace Application.Common;

public abstract class BaseCreateRequest<TDatabaseEntity> : IValidation, IRequest<BaseResult<CreateResponse>>
    where TDatabaseEntity : BaseEntity
{
    public abstract TDatabaseEntity BuildEntity();

    public virtual IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        yield break;
    }

    public virtual string ApiErrorMessagePrefix() => string.Empty;
}