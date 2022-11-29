using Microsoft.AspNetCore.Components;
using BogusStore.Shared.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using Append.Blazor.Sidepanel;

namespace BogusStore.Client.Products;

public partial class Index
{
    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
    [Parameter, SupplyParameterFromQuery] public decimal? MinPrice { get; set; }
    [Parameter, SupplyParameterFromQuery] public decimal? MaxPrice { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? TagId { get; set; }

    private IEnumerable<ProductDto.Index>? products;
    protected override async Task OnParametersSetAsync()
    {
        ProductRequest.Index request = new()
        {
            Searchterm = Searchterm,
            Page = Page.HasValue ? Page.Value : 1,
            PageSize = PageSize.HasValue ? PageSize.Value : 25,
            MinPrice = MinPrice,
            MaxPrice = MaxPrice,
            TagId = TagId
        };

        var response = await ProductService.GetIndexAsync(request);
        products = response.Products;
    }

    private void ShowCreateForm()
    {
        Sidepanel.Open<Components.Create>("Product", "Toevoegen");
    }
}
