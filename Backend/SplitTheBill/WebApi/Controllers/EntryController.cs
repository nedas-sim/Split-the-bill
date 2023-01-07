using Application.Finances.CreateEntry;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/entry")]
[ApiController]
public class EntryController : BaseController
{
    public EntryController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CreateResponse>> CreateEntry([FromBody] CreateEntryCommand command)
    {
        command.SetUserId(GetId());
        return FromResult(await sender.Send(command));
    }
}
