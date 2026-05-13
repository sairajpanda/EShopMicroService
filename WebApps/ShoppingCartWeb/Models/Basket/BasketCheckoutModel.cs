namespace ShoppingCartWeb.Models.Basket;

public class BasketCheckoutModel
{
    #region UserDetails
    public string UserName { get; set; } = default!;
    public Guid CustermId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;
    #endregion

    #region BillingAddressProperties
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string? EmailAddress { get; set; } = default!;
    #endregion

    #region PaymentProperties
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CardHolderName { get; set; } = default!;
    public DateTime ExpirationDate { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;
    #endregion
}

public record CheckOutBasketRequest(BasketCheckoutModel basketCheckoutDto);
public record CheckOutBasketResponse(bool IsSuccess);
