using Domain.Common.Results;
using Domain.Database;
using Domain.Responses.Users;
using MediatR;

namespace Application.Users.CreateUser;

public sealed record CreateUserCommand(string Username) : IRequest<BaseResult<UserResponse>>
{
    public string? GetValidationError()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            return "Username cannot be empty";
        }

        return null;
    }

    public User Create()
    {
        return new User
        {
            Id = UserId.Default,
            Username = Username,
        };
    }
}