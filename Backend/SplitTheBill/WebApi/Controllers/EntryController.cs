using Application.Finances.CreateEntry;
using Application.Finances.GetGroupEntries;
using Domain.Responses;
using Domain.Responses.Finances;
using Domain.Results;
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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ListResult<EntryResponse>>> GetEntries([FromQuery] GetGroupEntriesQuery query)
    {
        query.SetUserId(GetId());
        return FromListResult(await sender.Send(query));
    }
}
