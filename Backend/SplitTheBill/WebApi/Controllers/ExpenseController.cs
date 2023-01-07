using Application.Finances.GetEntryExpenses;
using Domain.Responses.Finances;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/expense")]
[ApiController]
public class ExpenseController : BaseController
{
    public ExpenseController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ListResult<EntryExpenseResponse>>> GetExpenses
        ([FromQuery] GetEntryExpensesQuery query)
    {
        query.SetUserId(GetId());
        return FromListResult(await sender.Send(query));
    }
}
