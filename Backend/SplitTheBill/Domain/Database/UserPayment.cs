namespace Domain.Database;

public sealed class UserPayment
{
    public UserId UserId { get; set; }
    public User User { get; set; }

    public PaymentId PaymentId { get; set; }
    public Payment Payment { get; set; }
}