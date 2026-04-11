using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Ordering.Application.Exception;

public class OrderNotFoundException : System.Exception
{
    public OrderNotFoundException(object orderId) : base($"Order with id {orderId} not found")
    {
    }
}
