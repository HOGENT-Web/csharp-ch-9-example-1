using BogusStore.Domain.Products;

namespace BogusStore.Fakers.Products;

public class ProductFaker : EntityFaker<Product>
{
    public ProductFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Product(f.Commerce.ProductName(), f.Commerce.ProductDescription(), new MoneyFaker(locale), f.Image.PicsumUrl()));
    }
}