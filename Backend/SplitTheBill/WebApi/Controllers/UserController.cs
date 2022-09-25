using Application.Users.CreateUser;
using Application.Users.GetUserById;
using Domain.Responses.Users;
using MediatR;
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

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Create([FromBody] CreateUserCommand command)
        => FromResult(await sender.Send(command));
}
