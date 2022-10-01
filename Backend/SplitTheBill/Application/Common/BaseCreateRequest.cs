using Domain.Common.Identity;
using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseCreateRequest<TDatabaseEntity, TId> : IRequest<BaseResult<Unit>>
    where TDatabaseEntity : BaseEntity<TId>
    where TId : DatabaseEntityId
{
    public virtual string? ValidateAndGetErrorMessage() => null;
    public abstract TDatabaseEntity BuildEntity();
}