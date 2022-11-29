using Application.Common;
using Domain.Responses.Authorization;

namespace Application.Authorization.Login;

public sealed class LoginQuery : IResultRequest<LoginResponse>
{
    #region API Params
    public string Email { get; set; }
    public string Password { get; set; }
    #endregion
}