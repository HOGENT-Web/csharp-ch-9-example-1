using BogusStore.Domain.Customers;

namespace BogusStore.Fakers.Common;

public class AddressFaker : Faker<Address>
{
    public AddressFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Address(f.Address.StreetAddress(), f.Address.SecondaryAddress().OrNull(f), f.Address.ZipCode(), f.Address.City(), f.Address.CountryCode()));
    }
}