using BogusStore.Shared.Products;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Products.Components;

public partial class Create
{
    private ProductDto.Mutate product = new();
    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private async Task CreateProductAsync()
    {
        ProductResult.Create result = await ProductService.CreateAsync(product);

        NavigationManager.NavigateTo($"product/{result.ProductId}");
    }
}