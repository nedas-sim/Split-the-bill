using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Exceptions;
using Domain.Responses;
using MediatR;

namespace Application.Common;

public abstract class BaseCreateRequest<TDatabaseEntity> : IRequest<BaseResult<CreateResponse>>
    where TDatabaseEntity : BaseEntity
{
    public virtual bool IsValid(out string? errorMessage)
    {
        // By default the request is valid
        errorMessage = null;
        return true;
    }

    public abstract TDatabaseEntity BuildEntity();

    public void ValidateAndThrow()
    {
        if (IsValid(out string? errorMessage) is false)
        {
            throw new ValidationErrorException(errorMessage!);
        }
    }
}