using Ordering.Domain.Enum;
namespace Ordering.Application.Dtos;

public record OrderDto(
List<OrderItemDto> OrderItems,
Guid CustomerId,
Guid Id,
string OrderName,
AddressDto BillingAddress,
AddressDto ShippingAddress,
PaymentDto Payment,
OrderStatus Status);

