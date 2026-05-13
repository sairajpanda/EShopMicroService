namespace ShoppingCartWeb.Models.Order;

public record OrderDto(
List<OrderItemDto> OrderItems,
Guid CustomerId,
Guid Id,
string OrderName,
AddressDto BillingAddress,
AddressDto ShippingAddress,
PaymentDto Payment,
OrderStatus Status);
public record OrderItemDto
    (Guid OrderId, Guid ProductId, int Quantity, decimal UnitPrice);

public record AddressDto(string FirstName, string LastName,
    string Street, string City, string State, string Country, string ZipCode,
    string EmailAddress);
public record PaymentDto(
    string CardName,
    string CardNumber,
    string CardHolderName,
    DateTime ExpirationDate,
    string Cvv,
    int PaymentMethod);
public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Completed = 3,
    Cancelled = 4,
    Updated = 5,
}

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public record GetProductsByNameResponse(IEnumerable<OrderDto> Orders);
public record GetOrdersCustomerResponse(IEnumerable<OrderDto> Orders);