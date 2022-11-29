using System;
using Append.Blazor.Sidepanel;
using BogusStore.Shared.Customers;
using BogusStore.Shared.Products;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Customers;

public partial class Index
{
    private IEnumerable<CustomerDto.Index>? customers;
    private string? searchTerm;

    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        CustomerRequest.Index request = new()
        {
            Page = 1,
            PageSize = 100,
            Searchterm = Searchterm
        };

        searchTerm = Searchterm;
        var response = await CustomerService.GetIndexAsync(request);
        customers = response.Customers;
    }

    private void SearchTermChanged(ChangeEventArgs args)
    {
        searchTerm = args.Value?.ToString();
        FilterCustomers();
    }

    private void FilterCustomers()
    {
        Dictionary<string, object?> parameters = new();

        parameters.Add(nameof(searchTerm), searchTerm);

        var uri = NavigationManager.GetUriWithQueryParameters(parameters);

        NavigationManager.NavigateTo(uri);
    }

    private void ShowCreateForm()
    {
        Sidepanel.Open<Components.Create>("Klant", "Toevoegen");
    }
}

