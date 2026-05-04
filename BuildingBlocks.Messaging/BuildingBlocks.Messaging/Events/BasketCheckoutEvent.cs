using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events;

public record BasketCheckoutEvent : IntegrationEvent
{
    public BasketCheckoutEvent(string userName, Guid custermId, decimal totalPrice, string firstName, string lastName, string street, string city, string state, string country, string zipCode, string? emailAddress, int paymentMethod, string cardName, string cardNumber, string cardHolderName, DateTime expirationDate, string cVV)
    {
        UserName = userName;
        CustermId = custermId;
        TotalPrice = totalPrice;
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        EmailAddress = emailAddress;
        PaymentMethod = paymentMethod;
        CardName = cardName;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpirationDate = expirationDate;
        CVV = cVV;
    }
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
