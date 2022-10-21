using Application.Common;
using Domain.Responses.Users;

namespace Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IResultRequest<UserResponse>
{
}