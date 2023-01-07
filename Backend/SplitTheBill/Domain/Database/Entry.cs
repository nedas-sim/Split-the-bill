using Domain.Common.Identity;

namespace Domain.Database;

public sealed class Entry : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public Guid GroupId { get; set; }
    public Group Group { get; set; }

    public ICollection<EntryExpense> Expenses { get; set; }
}
