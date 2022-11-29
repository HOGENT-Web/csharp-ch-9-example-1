namespace BogusStore.Fakers.Common;

public class MoneyFaker : Faker<Money>
{
	public MoneyFaker(string locale = "nl") : base(locale)
	{
        CustomInstantiator(f => new Money(f.Random.Decimal(1, 1_000)));
    }
}

