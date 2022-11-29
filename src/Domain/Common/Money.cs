namespace BogusStore.Domain.Common;

public class Money : ValueObject
{
    public decimal Value { get; }

    /// <summary>
    /// Database Constructor
    /// </summary>
    private Money() { }

    public Money(decimal value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Math.Round(Value, 2);
    }

    /// <summary>
    /// Copies the <see cref="Money"/> object to a new instance needed due to
    /// Github issue: https://github.com/dotnet/efcore/issues/12345
    /// </summary>
    /// <returns></returns>
    public Money Copy()
    {
        return new Money(Value);
    }

    public override string ToString() => Value.ToString("N2");
}
