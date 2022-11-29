using System;
using Append.Blazor.Sidepanel;
using BogusStore.Client.Customers.Components;
using BogusStore.Shared.Customers;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Customers;

public partial class Detail
{
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Parameter] public int Id { get; set; }
    private CustomerDto.Detail? customer;

    protected override async Task OnInitializedAsync()
    {
        await GetCustomerAsync();
    }

    private async Task GetCustomerAsync()
    {
        customer = await CustomerService.GetDetailAsync(Id);
    }

    private void ShowEditForm()
    {
        var callback = EventCallback.Factory.Create(this, GetCustomerAsync);

        var parameters = new Dictionary<string, object>
             {
                 { nameof(Edit.CustomerId), Id },
                 { nameof(Edit.OnCustomerEdited),callback  }
             };
        Sidepanel.Open<Edit>("Klant", "Wijzigen", parameters, BackdropType.Overlay);
    }
}

