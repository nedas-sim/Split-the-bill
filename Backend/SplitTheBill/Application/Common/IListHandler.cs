using Domain.Results;
using MediatR;

namespace Application.Common;

public interface IListHandler<TRequest, TResponse> : IRequestHandler<TRequest, ListResult<TResponse>>
    where TRequest : IListRequest<TResponse>
{
}