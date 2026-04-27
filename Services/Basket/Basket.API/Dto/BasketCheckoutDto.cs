namespace Basket.API.Dto;

public class BasketCheckoutDto
{
    #region UserDetails
    public string UserName { get; set; } = default!;
    public Guid CustermId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;
    #endregion

    #region BillingAddressProperties
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Street { get; init; } = default!;
    public string City { get; init; } = default!;
    public string State { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
    public string? EmailAddress { get; init; } = default!;
    #endregion

    #region PaymentProperties
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string CardHolderName { get; } = default!;
    public DateTime ExpirationDate { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;
    #endregion
}
