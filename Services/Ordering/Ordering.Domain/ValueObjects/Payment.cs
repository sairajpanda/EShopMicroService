
namespace Ordering.Domain.ValueObjects;

public class Payment
{
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string CardHolderName { get; } = default!;
    public DateTime ExpirationDate { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;
    protected Payment() { }
    private Payment(string cardName, string cardNumber, string cardHolderName, DateTime expirationDate, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpirationDate = expirationDate;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber, string cardHolderName, DateTime expirationDate, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrEmpty(cardHolderName, nameof(cardHolderName));
        ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
        if (expirationDate < DateTime.UtcNow)
        {
            throw new DomainException("Expiration date must be in the future");
        }
        return new Payment(cardName, cardNumber, cardHolderName, expirationDate, cvv, paymentMethod);
    }
}
