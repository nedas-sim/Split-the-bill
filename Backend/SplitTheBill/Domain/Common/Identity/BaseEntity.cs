namespace Domain.Common.Identity;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}