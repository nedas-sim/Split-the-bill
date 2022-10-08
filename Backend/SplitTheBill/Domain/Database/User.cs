using Domain.Common.Identity;

namespace Domain.Database;

public sealed class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<UserPayment> UserPayments { get; set; }
}