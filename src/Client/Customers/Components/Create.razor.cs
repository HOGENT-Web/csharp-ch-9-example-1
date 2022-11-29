using System;
using BogusStore.Shared.Customers;
using BogusStore.Shared.Products;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Customers.Components;

public partial class Create
{
    private readonly CustomerDto.Mutate customer = new();
    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private async Task CreateCustomerAsync()
    {
        int customerId = await CustomerService.CreateAsync(customer);
        Console.WriteLine("Customer created");
        NavigationManager.NavigateTo($"customer/{customerId}");
    }
}