using System;
using Blazored.FluentValidation;
using BogusStore.Client.Extensions;
using BogusStore.Client.Files;
using BogusStore.Shared.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BogusStore.Client.Products.Components;

public partial class Create
{
    private IBrowserFile? image;
    private ProductDto.Mutate product = new();
    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IStorageService StorageService { get; set; } = default!;

    private async Task CreateProductAsync()
    {
        ProductResult.Create result = await ProductService.CreateAsync(product);
        await StorageService.UploadImageAsync(result.UploadUri, image!);

        NavigationManager.NavigateTo($"product/{result.ProductId}");
    }

    private void LoadImage(InputFileChangeEventArgs e)
    {
        image = e.File;
        product.ImageContentType = image.ContentType;
    }
}