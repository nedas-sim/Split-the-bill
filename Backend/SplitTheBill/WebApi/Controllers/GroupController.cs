using Application.Groups.CreateGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/group")]
[ApiController]
public class GroupController : BaseController
{
    public GroupController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command)
    {
        Guid userId = GetId();
        command.SetUserId(userId);
        return ToNoContent(await sender.Send(command));
    }
}
