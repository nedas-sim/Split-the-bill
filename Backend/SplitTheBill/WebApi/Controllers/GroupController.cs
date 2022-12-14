using Application.Groups.CreateGroup;
using Application.Groups.GetFriendsForGroup;
using Application.Groups.GetUsersGroupList;
using Application.Groups.SendInvitation;
using Domain.Responses;
using Domain.Responses.Groups;
using Domain.Responses.Users;
using Domain.Results;
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
    public async Task<ActionResult<CreateResponse>> CreateGroup([FromBody] CreateGroupCommand command)
    {
        Guid userId = GetId();
        command.SetUserId(userId);
        return FromResult(await sender.Send(command));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ListResult<GroupResponse>>> GetUserGroups([FromQuery] GetUsersGroupListQuery query)
    {
        Guid userId = GetId();
        query.SetUserId(userId);
        return FromListResult(await sender.Send(query));
    }

    [HttpPost("invite")]
    [Authorize]
    public async Task<IActionResult> SendInvitation([FromBody] SendInvitationCommand command)
    {
        Guid userId = GetId();
        command.SetCallingUserId(userId);
        return ToNoContent(await sender.Send(command));
    }

    [HttpGet("friends/invitable")]
    [Authorize]
    public async Task<ActionResult<ListResult<UserResponse>>> GetInvitableFriendsForGroup([FromQuery] GetFriendsForGroupQuery query)
    {
        query.SetUserId(GetId());
        return FromListResult(await sender.Send(query));
    }
}
