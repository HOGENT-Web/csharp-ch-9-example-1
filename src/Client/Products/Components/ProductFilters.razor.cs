using System;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Products.Components;

public partial class ProductFilters
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter, EditorRequired] public string? Searchterm { get; set; } = default!;
    [Parameter, EditorRequired] public int? TagId { get; set; }
    [Parameter, EditorRequired] public decimal? MinPrice { get; set; }
    [Parameter, EditorRequired] public decimal? MaxPrice { get; set; }

    private string? searchTerm;
    private int? tagId ;
    private decimal? minPrice;
    private decimal? maxPrice;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        searchTerm = Searchterm;
        tagId = TagId;
        minPrice = MinPrice;
        maxPrice = MaxPrice;
    }

    private void SearchTermChanged(ChangeEventArgs args)
    {
        searchTerm = args.Value?.ToString();
        FilterProducts();
    }

    private void TagChanged(ChangeEventArgs args)
    {

        tagId = string.IsNullOrWhiteSpace(args.Value?.ToString()) ? null : Convert.ToInt32(args.Value?.ToString());
        FilterProducts();
    }

    private void MinPriceChanged(ChangeEventArgs args)
    {
        minPrice = string.IsNullOrWhiteSpace(args.Value?.ToString()) ? null : Convert.ToDecimal(args.Value!.ToString());
        FilterProducts();
    }

    private void MaxPriceChanged(ChangeEventArgs args)
    {
        maxPrice = string.IsNullOrWhiteSpace(args.Value?.ToString()) ? null : Convert.ToDecimal(args.Value!.ToString());
        FilterProducts();
    }

    private void FilterProducts()
    {
        Dictionary<string, object?> parameters = new();

        parameters.Add(nameof(searchTerm), searchTerm);
        parameters.Add(nameof(tagId), tagId);
        parameters.Add(nameof(minPrice), minPrice);
        parameters.Add(nameof(maxPrice), maxPrice);

        var uri = NavigationManager.GetUriWithQueryParameters(parameters);

        NavigationManager.NavigateTo(uri);
    }
}

