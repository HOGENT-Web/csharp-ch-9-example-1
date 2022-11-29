using System;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Orders;
using BogusStore.Domain.Products;
using BogusStore.Fakers.Customers;
using BogusStore.Fakers.Products;

namespace Domain.Tests.Orders;

public class Order_Should
{
    [Fact]
    public void Be_created_with_one_product()
    {
        Customer customer = new CustomerFaker();
        Product product = new ProductFaker();
        int quantity = 1;
        List<OrderItem> items = new();
        items.Add(new OrderItem(product, quantity));

        Order order = new Order(customer, items);

        order.Lines.Count.ShouldBe(1);
        OrderLine line = order.Lines.First();
        line.Product.ShouldBe(product);
        line.Quantity.ShouldBe(quantity);
        line.Price.ShouldBe(product.Price);
    }

    [Fact]
    public void Only_be_created_with_products()
    {
        Customer customer = new CustomerFaker();
        IEnumerable<OrderItem> items = Enumerable.Empty<OrderItem>();

        Action act = () =>
        {
            Order order = new Order(customer, items);
        };

        act.ShouldThrow<Exception>();
    }
}
