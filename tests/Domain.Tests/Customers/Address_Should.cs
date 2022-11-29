using BogusStore.Domain.Customers;
using Shouldly;

namespace BogusStore.Domain.Tests.Customers;

public class Address_Should
{
    [Fact]
    public void Be_created_when_valid()
    {
        const string street = "Arbeidstraat 14";
        const string postalCode = "9300";
        const string city = "Aalst";
        const string country = "BE";

        var address = new Address(street, null, postalCode, city, country);

        address.Addressline1.ShouldBe(street);
        address.PostalCode.ShouldBe(postalCode);
        address.City.ShouldBe(city);
        address.Country.ShouldBe(country);
    }

    [Fact]
    public void Throw_when_invalid_street()
    {
        const string? street = " ";
        const string postalCode = "9300";
        const string city = "Aalst";
        const string country = "BE";

        Action act = () =>
        {
            var address = new Address(street, null, postalCode, city, country);
        };

        act.ShouldThrow<Exception>();
    }
}
