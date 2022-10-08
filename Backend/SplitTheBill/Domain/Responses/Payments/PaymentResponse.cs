using Domain.Database;

namespace Domain.Responses.Payments;

public class PaymentResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }

    public PaymentResponse()
    {

    }

    public PaymentResponse(Payment payment)
    {
        Id = payment.Id.Id;
        Date = payment.DateOfPayment.Date;
        Amount = payment.Amount.Value;
        Currency = payment.Amount.Currency.ToString();
    }
}