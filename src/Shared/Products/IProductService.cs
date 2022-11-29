namespace BogusStore.Shared.Products;

public interface IProductService
{
    Task<ProductResult.Index> GetIndexAsync(ProductRequest.Index request);
    Task<ProductDto.Detail> GetDetailAsync(int productId);
    Task<ProductResult.Create> CreateAsync(ProductDto.Mutate model);
    Task EditAsync(int productId, ProductDto.Mutate model);
    Task DeleteAsync(int productId);
    Task AddTagAsync(int productId, int tagId);
}