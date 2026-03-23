
namespace Ordering.Domain.ValueObjects;

public class Payment
{
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string CardHolderName { get; } = default!;
    public DateTime ExpirationDate { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;
}
