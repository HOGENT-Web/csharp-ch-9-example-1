using System;
using Append.Blazor.Sidepanel;
using BogusStore.Client.Orders;
using BogusStore.Client.Orders.Components;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Layout;

public partial class Header : IDisposable
{
    private bool isOpen;
    private string? isOpenClass => isOpen ? "is-active" : null;

    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
    [Inject] public Cart Cart { get; set; } = default!;

    protected override void OnInitialized()
    {
        Cart.OnCartChanged += StateHasChanged;
    }

    public void Dispose()
    {
        Cart.OnCartChanged -= StateHasChanged;
    }

    private void ToggleMenuDisplay()
    {
        isOpen = !isOpen;
    }

    private void OpenShoppingCart()
    {
        Sidepanel.Open<ShoppingCart>("Winkelwagen");
    }
}

