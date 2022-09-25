namespace Domain.Common.Identity;

public abstract class DatabaseEntityId : ValueObject
{
    public Guid Id { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static implicit operator Guid(DatabaseEntityId id)
    {
        return id.Id;
    }
}