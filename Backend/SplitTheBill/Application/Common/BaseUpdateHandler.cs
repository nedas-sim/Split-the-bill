using Application.Repositories;
using Domain.Common;
using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Exceptions;
using Domain.Extensions;
using MediatR;

namespace Application.Common;

public abstract class BaseUpdateHandler<TRequest, TDatabaseEntity, TResponse> : IRequestHandler<TRequest, BaseResult<TResponse>>
    where TRequest : BaseUpdateRequest<TDatabaseEntity, TResponse>
    where TDatabaseEntity : BaseEntity
{
    private readonly IBaseRepository<TDatabaseEntity> repository;

    public BaseUpdateHandler(IBaseRepository<TDatabaseEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<BaseResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await PreValidation(request);
            (request as IValidation).ValidateAndThrow();
            TDatabaseEntity databaseEntity = await repository.GetById(request.Id, cancellationToken)
                ?? throw new NotFoundErrorException($"{typeof(TDatabaseEntity).Name} not found");

            await DatabaseValidation(request, databaseEntity, cancellationToken);

            request.Update(databaseEntity);
            await repository.Update(databaseEntity, cancellationToken);
            return await BuildResponse(request, databaseEntity);
        }
        catch (ValidationErrorException validationEx)
        {
            return validationEx.Message.ToValidationErrorResult<TResponse>();
        }
        catch (NotFoundErrorException notFoundEx)
        {
            return notFoundEx.Message.ToNotFoundResult<TResponse>();
        }
    }

    /// <summary>
    /// Called at the start
    /// </summary>
    public virtual async Task PreValidation(TRequest request)
    {
    }

    /// <summary>
    /// Called after request validation and entity retrieval by id
    /// </summary>
    public virtual async Task DatabaseValidation(TRequest request, TDatabaseEntity databaseEntity, CancellationToken cancellationToken)
    {
    }

    public abstract Task<TResponse> BuildResponse(TRequest request, TDatabaseEntity databaseEntity);
}