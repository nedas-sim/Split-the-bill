using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Exceptions;
using Domain.Results;
using MediatR;

namespace Application.Common;

public abstract class BaseCreateHandler<TRequest, TDatabaseEntity> : IRequestHandler<TRequest, BaseResult<Unit>>
    where TRequest : BaseCreateRequest<TDatabaseEntity>
    where TDatabaseEntity : BaseEntity
{
    public async Task<BaseResult<Unit>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            ThrowIfRequestIsInvalid(request);
            await DatabaseValidation(request, cancellationToken);
            TDatabaseEntity dbEntity = request.BuildEntity();
            await InsertionToDatabase(request, dbEntity, cancellationToken);

            return new NoContentResult<Unit>();
        }
        catch (ValidationErrorException validationEx)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = validationEx.Message,
            };
        }
    }

    private static void ThrowIfRequestIsInvalid(TRequest request)
    {
        if (request.IsValid(out string? errorMessage) is false)
        {
            throw new ValidationErrorException(errorMessage!);
        }
    }

    public virtual async Task DatabaseValidation(TRequest request, CancellationToken cancellationToken)
    {
        // by default there could be no database validation
    }

    public abstract Task InsertionToDatabase(TRequest request, TDatabaseEntity entity, CancellationToken cancellationToken);
}