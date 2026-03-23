using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class Customer : Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    internal Customer(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
