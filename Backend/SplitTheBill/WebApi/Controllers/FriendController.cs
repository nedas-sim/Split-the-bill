using Application.Friends.SendFriendRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/friend")]
[ApiController]
public class FriendController : BaseController
{
    public FriendController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> SendFriendRequest(SendFriendRequestCommand command)
    {
        Guid senderId = GetId();
        command.SetCallingUserId(senderId);
        return ToNoContent(await sender.Send(command));
    }
}
