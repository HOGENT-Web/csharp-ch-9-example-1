using System;
using Append.Blazor.Sidepanel;
using BogusStore.Shared.Products;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Products.Components;

public partial class Edit
{
    private ProductDto.Mutate product = new();

    [Parameter] public int ProductId { get; set; }
    [Parameter] public EventCallback OnProductEdited { get; set; }

    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var detail = await ProductService.GetDetailAsync(ProductId);
        product = new ProductDto.Mutate
        {
            Name = detail.Name,
            Description = detail.Description,
            Price = detail.Price
        };
    }

    private async Task EditProductAsync()
    {
        await ProductService.EditAsync(ProductId, product);
        Sidepanel.Close();
        await OnProductEdited.InvokeAsync();
    }
}