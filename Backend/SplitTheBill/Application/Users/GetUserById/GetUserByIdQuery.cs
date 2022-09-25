using Domain.Common.Results;
using Domain.Database;
using Domain.Responses.Users;
using MediatR;

namespace Application.Users.GetUserById;

public sealed record GetUserByIdQuery(UserId Id) : IRequest<BaseResult<UserResponse>>;