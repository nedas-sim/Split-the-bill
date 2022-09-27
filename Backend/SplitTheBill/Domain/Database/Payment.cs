using Domain.Common.Identity;

namespace Domain.Database;

public sealed class Payment : BaseEntity<PaymentId>
{
    public DateTime DateOfPayment { get; set; }
    public decimal Amount { get; set; }

    public ICollection<UserPayment> UserPayments { get; set; }
}

public sealed class PaymentId : DatabaseEntityId
{
    public static PaymentId Default => new() { Id = Guid.NewGuid() };
}