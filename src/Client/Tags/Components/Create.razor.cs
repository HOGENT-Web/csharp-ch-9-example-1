using System;
using BogusStore.Shared.Products;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Tags.Components;

public partial class Create
{
    private TagDto.Mutate tag = new();
    [Inject] public ITagService TagService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter] public EventCallback OnTagCreated { get; set; }

    private async Task CreateTagAsync()
    {
        int productId = await TagService.CreateAsync(tag);
        await OnTagCreated.InvokeAsync();
    }
}