using System;
using System.Net;
using BogusStore.Domain.Orders;

namespace BogusStore.Domain.Customers;

public class Customer : Entity
{
    private string firstname = default!;
    public string Firstname
    {
        get => firstname;
        set => firstname = Guard.Against.NullOrWhiteSpace(value, nameof(Firstname));
    }

    private string lastname = default!;
    public string Lastname
    {
        get => lastname;
        set => lastname = Guard.Against.NullOrWhiteSpace(value, nameof(Lastname));
    }

    private Address address = default!;
    public Address Address
    {
        get => address;
        set => address = Guard.Against.Null(value, nameof(Address));
    }

    private EmailAddress email = default!;
    public EmailAddress Email
    {
        get => email;
        set => email = Guard.Against.Null(value, nameof(Email));
    }

    private readonly List<Order> orders = new();
    public IReadOnlyCollection<Order> Orders => orders.AsReadOnly();

    /// <summary>
    /// Database Constructor
    /// </summary>
    private Customer() { }

    public Customer(string firstname, string lastname, Address address, EmailAddress email)
    {
        Firstname = firstname;
        Lastname = lastname;
        Address = address;
        Email = email;
    }

    public Order PlaceOrder(IEnumerable<OrderItem> items)
    {
        if (!IsEnabled)
            throw new ApplicationException($"{nameof(Customer)} is not active, could not place the order.");

        Order order = new Order(this, items);
        orders.Add(order);
        return order;
    }
}
