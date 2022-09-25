using Domain.Common.Identity;

namespace Domain.Database;

public sealed class User : BaseEntity<UserId>
{
    public string Username { get; set; }
}

public sealed class UserId : DatabaseEntityId
{
    public static implicit operator UserId(int id)
    {
        return new UserId
        {
            Id = id,
        };
    }
}