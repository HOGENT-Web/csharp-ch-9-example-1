using System;
namespace BogusStore.Shared.Products;

public abstract class ProductResult
{
    public class Index
    {
        public IEnumerable<ProductDto.Index>? Products { get; set; }
        public int TotalAmount { get; set; }
    }

    public class Create
    {
        public int ProductId { get; set; }
        public string UploadUri { get; set; } = default!;
    }
}
