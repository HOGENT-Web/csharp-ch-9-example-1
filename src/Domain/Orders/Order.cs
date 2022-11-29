using System;
using BogusStore.Domain.Customers;

namespace BogusStore.Domain.Orders;

public class Order : Entity
{
    private readonly List<OrderLine> lines = new();

    public Customer Customer { get; } = default!;

    public IReadOnlyCollection<OrderLine> Lines => lines.AsReadOnly();

    private Order() { }

    public Order(Customer customer, IEnumerable<OrderItem> items)
    {
        Customer = Guard.Against.Null(customer, nameof(Customer));
        Guard.Against.NullOrEmpty(items, nameof(items));

        foreach (OrderItem item in items)
        {
            lines.Add(new OrderLine(this, item));
        }
    }
}