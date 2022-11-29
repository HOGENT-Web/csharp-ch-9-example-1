using System;
using Append.Blazor.Sidepanel;
using BogusStore.Shared.Customers;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Customers.Components;

public partial class Edit
{
    private CustomerDto.Mutate customer = new();

    [Parameter] public int CustomerId { get; set; }
    [Parameter] public EventCallback OnCustomerEdited { get; set; }

    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var detail = await CustomerService.GetDetailAsync(CustomerId);
        customer = new CustomerDto.Mutate
        {
            Email = detail.Email,
            Firstname = detail.Firstname,
            Lastname = detail.Lastname,
            Address = new AddressDto
            {
                Addressline1 = detail.Address.Addressline1,
                Addressline2 = detail.Address.Addressline2,
                City = detail.Address.City,
                Country = detail.Address.Country,
                PostalCode = detail.Address.PostalCode
            }
        };
    }

    private async Task EditCustomerAsync()
    {
        await CustomerService.EditAsync(CustomerId, customer);
        Sidepanel.Close();
        await OnCustomerEdited.InvokeAsync();
    }
}

