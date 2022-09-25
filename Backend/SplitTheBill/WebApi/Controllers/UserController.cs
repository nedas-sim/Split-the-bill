using Application.Users.GetUserById;
using Domain.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UserController : BaseController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetById(int id)
        => FromResult(await sender.Send(new GetUserByIdQuery(id)));
}
