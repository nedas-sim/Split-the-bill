using Application.Payments.CreatePayment;
using Application.Payments.DeletePayment;
using Application.Payments.GetPaymentById;
using Application.Payments.GetPaymentList;
using Application.Payments.UpdatePayment;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentResponse>> GetById([FromRoute] Guid id)
        => FromResult(await sender.Send(new GetPaymentByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
        => ToNoContent(await sender.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePaymentCommand command)
    {
        command.SetId(id);
        return ToNoContent(await sender.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
        => ToNoContent(await sender.Send(new DeletePaymentCommand(id)));
}
