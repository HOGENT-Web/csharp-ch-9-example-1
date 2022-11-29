using System;
namespace BogusStore.Shared.Products;

public abstract class TagRequest
{
    public class Index
    {
        public string? Searchterm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}

