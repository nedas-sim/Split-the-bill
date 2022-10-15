using Domain.Common.Results;
using Domain.Responses.Authorization;
using MediatR;

namespace Application.Authorization.Login;

public sealed class LoginQuery : IRequest<BaseResult<LoginResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}