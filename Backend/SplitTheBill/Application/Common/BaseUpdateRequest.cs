using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Exceptions;
using MediatR;

namespace Application.Common;

public abstract class BaseUpdateRequest<TDatabaseEntity> : IRequest<BaseResult<Unit>>
    where TDatabaseEntity : BaseEntity
{
    public Guid Id { get; internal set; }
    public void SetId(Guid id) => Id = id;

    public abstract void Update(TDatabaseEntity databaseEntity);
    public virtual bool IsValid(out string? errorMessage)
    {
        // By default the request is valid
        errorMessage = null;
        return true;
    }

    public void ValidateAndThrow()
    {
        if (IsValid(out string? errorMessage) is false)
        {
            throw new ValidationErrorException(errorMessage!);
        }
    }
}