using Microsoft.AspNetCore.Components;
using BogusStore.Shared.Products;
using System.Threading.Tasks;
using BogusStore.Client.Products.Components;
using Append.Blazor.Sidepanel;
using BogusStore.Client.Orders;

namespace BogusStore.Client.Products;

public partial class Detail
{
    private ProductDto.Detail? product;
    private bool isRequestingDelete;

    [Parameter] public int Id { get; set; }
    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
    [Inject] public Cart Cart { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        await GetProductAsync();
    }

    private void RequestDelete()
    {
        isRequestingDelete = true;
    }

    private void CancelDeleteRequest()
    {
        isRequestingDelete = false;
    }

    private async Task DeleteProductAsync()
    {
        await ProductService.DeleteAsync(Id);
        NavigationManager.NavigateTo("product");
    }

    private void ShowEditForm()
    {
        var callback = EventCallback.Factory.Create(this, GetProductAsync);

        var parameters = new Dictionary<string, object>
             {
                 { nameof(Edit.ProductId), Id },
                 { nameof(Edit.OnProductEdited),callback  }
             };
        Sidepanel.Open<Edit>("Product", "Wijzigen", parameters);
    }

    private async Task GetProductAsync()
    {
        product = await ProductService.GetDetailAsync(Id);
    }

    private void AddToCart()
    {
        Cart.AddItem(product!.Id, product!.Name!, product.Price);
    }
}
