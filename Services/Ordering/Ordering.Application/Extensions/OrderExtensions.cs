using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ProjectToOrderDto
        (this IEnumerable<Order> order)
    {
        List<OrderDto> _orderDtoList = new List<OrderDto>();

        foreach (var item in order)
        {
            var OrderDto = new OrderDto(
                  Id: item.Id.Value,
                  CustomerId: item.CustomerId.Value,
                  OrderName: item.OrderName.Value,
                  BillingAddress: new AddressDto
                  (
                      FirstName: item.BillingAddress.FirstName,
                      LastName: item.BillingAddress.LastName,
                      Street: item.BillingAddress.Street,
                      City: item.BillingAddress.City,
                      State: item.BillingAddress.State,
                      ZipCode: item.BillingAddress.ZipCode,
                      Country: item.BillingAddress.Country,
                      EmailAddress: string.IsNullOrEmpty(item.BillingAddress.EmailAddress) ? "Noemail@gmail.com" : item.BillingAddress.EmailAddress
                  ),
                  ShippingAddress: new AddressDto
                  (
                        FirstName: item.ShippingAddress.FirstName,
                      LastName: item.ShippingAddress.LastName,
                      Street: item.ShippingAddress.Street,
                      City: item.ShippingAddress.City,
                      State: item.ShippingAddress.State,
                      ZipCode: item.ShippingAddress.ZipCode,
                      Country: item.ShippingAddress.Country,
                      EmailAddress: string.IsNullOrEmpty(item.ShippingAddress.EmailAddress) ? "Noemail@gmail.com" : item.ShippingAddress.EmailAddress
                  ),
                  Payment: new PaymentDto
                  (
                      CardNumber: item.payment.CardNumber,
                      CardName: item.payment.CardName,
                      CardHolderName: item.payment.CardHolderName,
                      ExpirationDate: item.payment.ExpirationDate,
                      Cvv: item.payment.CVV,
                      PaymentMethod: item.payment.PaymentMethod
                  ),
                  Status: item.Status,
                  OrderItems: item.OrderItems.Select(oi => new OrderItemDto(
                      OrderId: oi.Id.Value,
                      ProductId: oi.ProductId.Value,
                      Quantity: oi.Quantity,
                      UnitPrice: oi.UnitPrice
                  )).ToList()
              );
            _orderDtoList.Add(OrderDto);
        }
        return _orderDtoList;
    }
}