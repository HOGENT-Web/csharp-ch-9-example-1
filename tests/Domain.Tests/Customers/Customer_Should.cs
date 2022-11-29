using System;
using Bogus;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Products;
using BogusStore.Fakers.Customers;
using BogusStore.Fakers.Products;

namespace BogusStore.Domain.Tests.Customers;

public class Customer_Should
{
    [Fact]
    public void Be_created_when_valid()
    {
        string firstname = "Sjarelken";
        string lastname = "Van Der Willem";
        Address address = new AddressFaker();
        EmailAddress email = new EmailAddress("Sjarelken@gmail.com");

        var customer = new Customer(firstname,lastname, address, email);

        customer.Firstname.ShouldBe(firstname);
        customer.Lastname.ShouldBe(lastname);
        customer.Address.ShouldNotBeNull();
    }

    [Fact]
    public void Be_created_when_valid_with_an_inline_faker()
    {
        // Inline faker example
        Person person = new Faker().Person;
        string firstname = person.FirstName;
        string lastname = person.LastName;
        EmailAddress email = new EmailAddress(person.Email);

        Address address = new AddressFaker();

        var customer = new Customer(firstname, lastname, address,email);

        customer.Firstname.ShouldBe(firstname);
        customer.Lastname.ShouldBe(lastname);
        customer.Address.ShouldNotBeNull();
    }

    [Fact]
    public void Throw_when_invalid_firstname()
    {
        string firstname = "  ";
        string lastname = "Van Der Willem";
        Address address = new AddressFaker();
        EmailAddress email = new EmailAddress("Sjarelken@gmail.com");

        Action act = () =>
        {
            var customer = new Customer(firstname, lastname, address,email);
        };

        act.ShouldThrow<Exception>();
    }
}

