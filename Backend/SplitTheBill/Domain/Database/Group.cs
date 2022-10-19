using Domain.Common.Identity;

namespace Domain.Database;

public sealed class Group : BaseEntity
{
    public string Name { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; }
}