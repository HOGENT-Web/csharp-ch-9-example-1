using BogusStore.Domain.Customers;

namespace BogusStore.Fakers.Customers;

public class CustomerFaker : EntityFaker<Customer>
{
    public CustomerFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Customer(f.Person.FirstName, f.Person.LastName, new AddressFaker(locale), new EmailAddress(f.Person.Email)));
    }
}
