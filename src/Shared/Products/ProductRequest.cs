namespace BogusStore.Shared.Products;

public abstract class ProductRequest
{
    public class Index
    {
        public string? Searchterm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? TagId { get; set; }
    }
}

