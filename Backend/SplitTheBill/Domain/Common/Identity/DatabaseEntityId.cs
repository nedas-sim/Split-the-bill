namespace Domain.Common.Identity;

public abstract class DatabaseEntityId : ValueObject
{
    public int Id { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static implicit operator int(DatabaseEntityId id)
    {
        return id.Id;
    }
}