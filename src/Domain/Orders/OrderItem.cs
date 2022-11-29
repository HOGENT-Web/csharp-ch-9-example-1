using System;
using BogusStore.Domain.Products;

namespace BogusStore.Domain.Orders;

public class OrderItem : ValueObject
{
    public Product Product { get; }
    public int Quantity { get; }

    public OrderItem(Product product, int quantity)
    {
        Product = Guard.Against.Null(product, nameof(Product));
        Quantity = Guard.Against.Zero(quantity, nameof(quantity));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Product;
    }
}

