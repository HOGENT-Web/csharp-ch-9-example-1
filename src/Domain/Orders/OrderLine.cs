using System;
using BogusStore.Domain.Products;

namespace BogusStore.Domain.Orders;

public class OrderLine : Entity
{
    public Order Order { get; } = default!;
    public Product Product { get; } = default!;
    public int Quantity { get; }
    public Money Price { get; } = default!;
    public string Description { get; } = default!;

    private OrderLine() { }

    public OrderLine(Order order, OrderItem item)
    {
        Order = Guard.Against.Null(order, nameof(Order));
        Guard.Against.Null(item, nameof(OrderItem));
        Quantity = item.Quantity;
        Product = item.Product;
        Price = Product.Price.Copy(); // https://github.com/dotnet/efcore/issues/12345
        Description = Product.Name;
    }
}

