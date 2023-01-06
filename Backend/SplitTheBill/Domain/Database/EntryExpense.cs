using Domain.Common.Identity;

namespace Domain.Database;

public sealed class EntryExpense : BaseEntity
{
    public decimal Amount { get; set; }

    public Guid PayerId { get; set; }
    public User Payer { get; set; }

    public Guid DebtorId { get; set; }
    public User Debtor { get; set; }

    public Guid EntryId { get; set; }
    public Entry Entry { get; set; }
}
