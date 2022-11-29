using System;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Orders;
using BogusStore.Domain.Products;
using BogusStore.Persistence;
using BogusStore.Shared.Orders;
using BogusStore.Shared.Products;
using Microsoft.EntityFrameworkCore;

namespace BogusStore.Services.Orders;

public class OrderService : IOrderService
{
    private readonly BogusDbContext dbContext;

    public OrderService(BogusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(int customerId, OrderDto.Create model)
    {
        Customer? customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId);

        if (customer is null)
            throw new EntityNotFoundException(nameof(Customer), customerId);

        IEnumerable<Product> products = await dbContext.Products
                                                .Where(x => model.Items.Select(x => x.ProductId).Contains(x.Id))
                                                .ToListAsync();

        List<OrderItem> orderItems = new();

        foreach (var item in model.Items)
        {
            Product? p = products.FirstOrDefault(x => x.Id == item.ProductId);

            if (p is null)
                throw new EntityNotFoundException(nameof(Product), item.ProductId);
            orderItems.Add(new OrderItem(p, item.Quantity));
        }

        Order order = customer.PlaceOrder(orderItems);
        await dbContext.SaveChangesAsync();

        return order.Id;
    }
}

