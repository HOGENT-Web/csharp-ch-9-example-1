using BogusStore.Domain.Products;
using BogusStore.Fakers.Customers;
using BogusStore.Fakers.Products;

namespace BogusStore.Persistence;

public class FakeSeeder
{
    private readonly BogusDbContext dbContext;

    public FakeSeeder(BogusDbContext dbContext)
	{
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        SeedProducts();
        SeedTags();
        SeedCustomers();
    }

    private void SeedProducts()
	{
        var products = new ProductFaker().AsTransient().UseSeed(1337).Generate(100);
        dbContext.Products.AddRange(products);
        dbContext.SaveChanges();
    }

    private void SeedTags()
    {
        List<Tag> tags = new()
        {
            new Tag("Vers", "green"),
            new Tag("Bevroren", "blue"),
            new Tag("Hout","red"),
            new Tag("Rubber","orange"),
            new Tag("Metaal","gray"),
        };

        dbContext.Tags.AddRange(tags);
        dbContext.SaveChanges();
    }

    private void SeedCustomers()
    {
        var customers = new CustomerFaker().AsTransient().UseSeed(1337).Generate(10);
        dbContext.Customers.AddRange(customers);
        dbContext.SaveChanges();
    }
}

