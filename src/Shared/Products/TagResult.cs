using System;
namespace BogusStore.Shared.Products;

public abstract class TagResult
{
    public class Index
    {
        public IEnumerable<TagDto.Index>? Tags { get; set; }
        public int TotalAmount { get; set; }
    }
}

