namespace Domain.Database;

public sealed class UserPayment
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; }
}