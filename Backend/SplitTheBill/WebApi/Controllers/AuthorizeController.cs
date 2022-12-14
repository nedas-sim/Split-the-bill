using Application.Authorization.Login;
using Application.Authorization.Registration;
using Domain.Common.Results;
using Domain.Responses.Authorization;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthorizeController : BaseController
{
    public AuthorizeController(ISender sender) : base(sender)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        => ToNoContent(await sender.Send(command));

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        BaseResult<LoginResponse> loginResponse = await sender.Send(query);

        if (loginResponse is SuccessResult<LoginResponse> successResult)
        {
            SetJwt(successResult.Item.Jwt);
        }

        return ToNoContent(loginResponse);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        RemoveJwt();
        return NoContent();
    }

    [HttpPost("isLoggedIn")]
    public async Task<IActionResult> IsLoggedIn()
    {
        return JwtExists()
            ? Ok()
            : BadRequest();
    }

    #region Private Methods
    private void SetJwt(string jwt)
    {
        CookieOptions cookieOptions = new()
        {
            HttpOnly = true,
        };

        Response.Cookies.Append(JwtCookieKey, jwt, cookieOptions);
    }

    private void RemoveJwt()
    {
        Response.Cookies.Delete(JwtCookieKey);
    }

    private bool JwtExists()
    {
        return Request.Cookies[JwtCookieKey] is not null;
    }
    #endregion
}
