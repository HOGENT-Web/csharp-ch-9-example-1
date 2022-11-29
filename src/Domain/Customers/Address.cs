namespace BogusStore.Domain.Customers;

public class Address : ValueObject
{
    public string Addressline1 { get; } = default!;
    public string? Addressline2 { get; }
    public string PostalCode { get; } = default!;
    public string City { get; } = default!;
    public string Country { get; } = default!;

    /// <summary>
    /// Database Constructor
    /// </summary>
    private Address() { }

    public Address(string addressline1, string? addressline2, string postalCode, string city, string country)
    {
        Addressline1 = Guard.Against.NullOrWhiteSpace(addressline1, nameof(Addressline1));
        Addressline2 = addressline2;
        PostalCode = Guard.Against.NullOrWhiteSpace(postalCode, nameof(PostalCode));
        City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
        Country = Guard.Against.NullOrWhiteSpace(country, nameof(Country)).ToUpper();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Country.ToLower();
        yield return PostalCode.ToLower();
        yield return City.ToLower();
        yield return Addressline1.ToLower();
        yield return Addressline2?.ToLower();
    }

    public override string ToString()
    {
        return $"{Addressline1},{PostalCode}-{City} {Country}";
    }
}


