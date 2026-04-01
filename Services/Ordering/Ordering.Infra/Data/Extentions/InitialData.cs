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
            Customer.Create(CustomerId.Of(new Guid("60b1f787-e9b3-459a-8f1e-a28a125c02e9")),"Payel","Payel@gmail")
        };
}
