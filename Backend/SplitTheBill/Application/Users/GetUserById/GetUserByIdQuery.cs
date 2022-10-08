using Domain.Common.Results;
using Domain.Responses.Users;
using MediatR;

namespace Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<BaseResult<UserResponse>>
{
}