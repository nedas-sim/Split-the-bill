using Domain.Common.Identity;
using Domain.Enums;

namespace Domain.Database;

public sealed class Payment : BaseEntity
{
    public DateTime DateOfPayment { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    public ICollection<UserPayment> UserPayments { get; set; }
}