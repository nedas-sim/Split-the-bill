using Domain.Common.Identity;

namespace Domain.Database;

public sealed class User : BaseEntity<UserId>
{
    public string Username { get; set; }
}

public sealed class UserId : DatabaseEntityId
{
    public static UserId Default => new() { Id = Guid.NewGuid() };
}