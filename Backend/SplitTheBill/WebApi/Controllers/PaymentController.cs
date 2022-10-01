using Application.Payments.GetPaymentById;
using Application.Payments.GetPaymentList;
using Domain.Responses.Payments;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/payment")]
[ApiController]
public class PaymentController : BaseController
{
    public PaymentController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentResponse>>> GetList([FromQuery] GetPaymentListQuery query) 
        => Ok(await sender.Send(query));

    [HttpGet("id")]
    public async Task<ActionResult<PaymentResponse>> GetById([FromQuery] Guid id)
        => FromResult(await sender.Send(new GetPaymentByIdQuery(id)));
}
