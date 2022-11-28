using Domain.Common;
using Domain.Common.Results;
using Domain.Results;
using MediatR;

namespace Application.Common;

public interface IListHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseListResult<TResponse>>
    where TRequest : IListRequest<TResponse>
{
}

public abstract class BaseListHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseListResult<TResponse>>
    where TRequest : IListRequest<TResponse>
{
    public async Task<BaseListResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await PreValidation(request);

        if (request is IValidation validationRequest)
        {
            validationRequest.ValidateAndThrow();
        }

        List<TResponse> responses = await GetResponses(request, cancellationToken);
        int totalCount = await GetTotalCount(request, cancellationToken);

        return new ListResult<TResponse>(responses, totalCount, request);
    }

    /// <summary>
    /// Called at the start
    /// </summary>
    public virtual async Task PreValidation(TRequest request)
    {
    }

    public abstract Task<List<TResponse>> GetResponses(TRequest request, CancellationToken cancellationToken);
    public abstract Task<int> GetTotalCount(TRequest request, CancellationToken cancellationToken);
}