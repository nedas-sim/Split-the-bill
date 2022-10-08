using Application.Authorization.Registration;
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
}
