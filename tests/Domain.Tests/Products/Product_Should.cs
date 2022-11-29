using System;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Products;
using BogusStore.Fakers.Customers;
using BogusStore.Fakers.Products;

namespace BogusStore.Domain.Tests.Products;

public class Product_Should
{
    [Fact]
    public void Be_created_when_valid()
    {
        string name = "Football";
        string description = "Something round to kick on.";
        Money price = new MoneyFaker();

        Product product = new(name, description,price,"fake image");

        product.Name.ShouldBe(name);
        product.Description.ShouldBe(description);
        product.Price.ShouldBe(price);
    }

    [Fact]
    public void Throw_when_invalid_description()
    {
        string name = "Football";
        string description = string.Empty;
        Money price = new MoneyFaker();

        Action act = () =>
        {
            Product product = new(name, description, price, "fake image");
        };

        act.ShouldThrow<Exception>();
    }

    [Fact]
    public void Add_new_tag()
    {
        Product product = new ProductFaker();
        Tag tag = new TagFaker();

        product.Tag(tag);

        product.Tags.Count.ShouldBe(1);
        product.Tags.First().ShouldBe(tag);

    }

    [Fact]
    public void Throw_when_tag_already_exists()
    {
        Product product = new ProductFaker();
        Tag tag = new TagFaker();

        product.Tag(tag);

        Action act = () =>
        {
            product.Tag(tag);
        };

        act.ShouldThrow<ApplicationException>();
    }
}

