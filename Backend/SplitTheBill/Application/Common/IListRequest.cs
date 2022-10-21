using Domain.Results;
using MediatR;

namespace Application.Common;

public interface IListRequest<TResponse> : IRequest<ListResult<TResponse>>
{
}