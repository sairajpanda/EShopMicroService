using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infra.Data.Extentions;

public class InitialData
{
    public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c02e7")),"Sairaj","Sairaj@gmail"),
            Customer.Create(CustomerId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c02e8")),"Payel","Payel@gmail")
        };

    public static IEnumerable<Product> Products =>
    new List<Product>
    {
            Product.Create(ProductId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c02e9")),"Iphone",12000),
            Product.Create(ProductId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c0210")),"SamSung",2000),
            Product.Create(ProductId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c0211")),"Nokia",4000),
            Product.Create(ProductId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c0212")),"Xiomu",8000)
    };

    public static IEnumerable<Order> OrderWithItems
    {
        get
        {
            var address1 = Address.Of("Sai1","raj1","Tamando1","BBSR1","ODisha1","IN1","752054","sai@gmail1");
            var payment1 = Payment.Of("Sai1","123123123123","Sai1",DateTime.UtcNow.AddYears(4),"232",1);

            var order1 = Order.Create(
                         OrderId.Of(Guid.NewGuid()),
                         CustomerId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c02e7")),
                         OrderName.Of("Order 1"),
                         shippingAddress: address1,
                         billingAddress: address1,
                         payment: payment1
                         );
            order1.Add(ProductId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c02e9")), 1, 12000);
            order1.Add(ProductId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c0210")), 1, 2000);

            return new List<Order> { order1 };
        }
    }
}
