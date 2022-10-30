using Application.Users.GetUserById;
using Application.Users.GetUserList;
using Application.Users.Update;
using Domain.Responses.Users;
using Domain.Results;
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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ListResult<UserResponse>>> GetUserList([FromQuery] GetUserListQuery query)
    {
        Guid id = GetId();
        query.SetCallingUserId(id);
        return FromListResult(await sender.Send(query));
    }
}
