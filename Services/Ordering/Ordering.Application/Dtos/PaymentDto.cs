

namespace Ordering.Application.Dtos;

public record PaymentDto(string CardName, string CardNumber,
    string CardHolderName, DateTime ExpirationDate, string Cvv, 
    int PaymentMethod);
