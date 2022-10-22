using Application.Users.GetUserById;
using Application.Users.Update;
using Domain.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/user")]
[ApiController]
public sealed class UserController : BaseController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetById([FromRoute] Guid id)
        => FromResult(await sender.Send(new GetUserByIdQuery(id)));

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        Guid id = GetId();
        command.SetId(id);
        return ToNoContent(await sender.Send(command));
    }
}
