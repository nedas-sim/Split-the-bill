using Domain.Database;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

/// <summary>
/// For testing purposes only.
/// </summary>
[Route("api/test")]
[ApiController]
public class TestController : BaseController
{
    private readonly DataContext context;

    public TestController(ISender sender, DataContext context) : base(sender)
    {
        this.context = context;
    }

    [HttpPost]
    [Route("payment")]
    public async Task<IActionResult> AddRandomPayment()
    {
        /*Payment payment = new()
        {
            Id = PaymentId.Default,
            DateOfPayment = DateTime.UtcNow,
            Amount = 1.23m,
        };

        context.Payments.Add(payment);
        await context.SaveChangesAsync();*/

        return Ok();
    }

    [HttpPost]
    [Route("userPayment")]
    public async Task<IActionResult> AddUserPayment()
    {
        User? user = await context.Users.FirstOrDefaultAsync();
        Payment? payment = await context.Payments.OrderBy(x => x.DateOfPayment).LastOrDefaultAsync();

        if (user is null || payment is null)
        {
            return BadRequest();
        }

        UserPayment up = new()
        {
            PaymentId = payment.Id,
            UserId = user.Id,
        };

        context.UserPayments.Add(up);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("firstUsersPayments")]
    public async Task<IActionResult> GetFirstUsersPayments()
    {
        User? user = await context.Users.FirstOrDefaultAsync();

        if (user is null)
        {
            return BadRequest();
        }

        List<Payment> payments =
            await context.Payments
                         .Where(p => p.UserPayments.Any(up => up.UserId == user.Id))
                         .ToListAsync();

        return Ok(payments);
    }

    [HttpGet]
    [Route("authPing")]
    [Authorize]
    public async Task<IActionResult> AuthPing()
    {
        Guid id = GetId();
        return Ok(new
        {
            Id = id,
        });
    }
}
