using System;
using BogusStore.Domain.Products;

namespace BogusStore.Fakers.Products;

public class TagFaker : EntityFaker<Tag>
{
    public TagFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Tag(f.PickRandom(f.Commerce.Categories(10)),f.Commerce.Color()));
    }
}