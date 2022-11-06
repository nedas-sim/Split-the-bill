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
    [Route("friendship")]
    public async Task<IActionResult> AddFriendships()
    {
        var users = await context.Users.ToListAsync();
        var firstUser = users[0];

        for (int index = 1; index < users.Count; index++)
        {
            var user = users[index];

            var friendship = new UserFriendship
            {
                RequestSenderId = firstUser.Id,
                RequestReceiverId = user.Id,
                InvitedOn = DateTime.UtcNow,
            };

            if (index % 2 == 0)
            {
                friendship.AcceptedOn = friendship.InvitedOn.AddHours(1);
            }

            context.UserFriendships.Add(friendship);
        }

        await context.SaveChangesAsync();

        return Ok();
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
