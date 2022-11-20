﻿using Application.Repositories;
using Domain.Common.Identity;
using Domain.Common.Results;
using Domain.Exceptions;
using Domain.Results;
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
            request.ValidateAndThrow();
            TDatabaseEntity databaseEntity = await repository.GetById(request.Id, cancellationToken)
                ?? throw new NotFoundErrorException($"{typeof(TDatabaseEntity).Name} not found");

            await DatabaseValidation(request, databaseEntity, cancellationToken);

            request.Update(databaseEntity);
            await repository.Update(databaseEntity, cancellationToken);
            return await BuildResponse(request, databaseEntity);
        }
        catch (ValidationErrorException validationEx)
        {
            return new ValidationErrorResult<TResponse>
            {
                Message = validationEx.Message,
            };
        }
        catch (NotFoundErrorException notFoundEx)
        {
            return new NotFoundResult<TResponse>
            {
                Message = notFoundEx.Message,
            };
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