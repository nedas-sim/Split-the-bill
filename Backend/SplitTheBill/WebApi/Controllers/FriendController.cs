using Application.Friends.GetFriendList;
using Application.Friends.GetRequestList;
using Application.Friends.SendFriendRequest;
using Application.Friends.UpdateFriendRequest;
using Domain.Responses.Users;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/friend")]
[ApiController]
public class FriendController : BaseController
{
    public FriendController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SendFriendRequest([FromBody] SendFriendRequestCommand command)
    {
        Guid senderId = GetId();
        command.SetCallingUserId(senderId);
        return ToNoContent(await sender.Send(command));
    }

    [Authorize]
    [HttpGet("request")]
    public async Task<ActionResult<ListResult<UserResponse>>> GetFriendRequests([FromQuery] GetRequestListQuery query)
    {
        Guid receiverId = GetId();
        query.SetCallingUserId(receiverId);
        return FromListResult(await sender.Send(query));
    }

    [Authorize]
    [HttpPut("request")]
    public async Task<IActionResult> UpdateFriendRequest([FromBody] UpdateFriendRequestCommand command)
    {
        Guid receiverId = GetId();
        command.SetReceiverId(receiverId);
        return ToNoContent(await sender.Send(command));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ListResult<UserResponse>>> GetFriendList([FromQuery] GetFriendListQuery query)
    {
        Guid userId = GetId();
        query.SetCallingUserId(userId);
        return FromListResult(await sender.Send(query));
    }
}
