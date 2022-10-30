using Domain.Common.Results;
using MediatR;

namespace Application.Common;

public interface IListHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseListResult<TResponse>>
    where TRequest : IListRequest<TResponse>
{
}