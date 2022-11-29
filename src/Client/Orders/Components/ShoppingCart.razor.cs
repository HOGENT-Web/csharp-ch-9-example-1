using System;
using Append.Blazor.Sidepanel;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Orders.Components;

public partial class ShoppingCart : IDisposable
{
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
}

