namespace Domain.Common.Identity;

public abstract class BaseEntity<TId> where TId : DatabaseEntityId
{
    public TId Id { get; set; }
}