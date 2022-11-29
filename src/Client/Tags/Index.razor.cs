using System;
using Append.Blazor.Sidepanel;
using BogusStore.Client.Products.Components;
using BogusStore.Client.Tags.Components;
using BogusStore.Shared.Products;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Tags;

public partial class Index
{
    private IEnumerable<TagDto.Index>? tags;
    private string? searchTerm;

    [Inject] public ITagService TagService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        TagRequest.Index request = new()
        {
            Page = 1,
            PageSize = 100,
            Searchterm = Searchterm
        };

        searchTerm = Searchterm;

        var response = await TagService.GetIndexAsync(request);
        tags = response.Tags;
    }

    private void SearchTermChanged(ChangeEventArgs args)
    {
        searchTerm = args.Value?.ToString();
        FilterTags();
    }

    private void FilterTags()
    {
        Dictionary<string, object?> parameters = new();

        parameters.Add(nameof(searchTerm), searchTerm);

        var uri = NavigationManager.GetUriWithQueryParameters(parameters);

        NavigationManager.NavigateTo(uri);
    }

    private void ShowCreateForm()
    {
        var callback = EventCallback.Factory.Create(this, async _ =>
        {
            var response = await TagService.GetIndexAsync(new TagRequest.Index());
            tags = response.Tags;
            Sidepanel.Close();
        });
        Sidepanel.Open<Components.Create>("Tag", "Toevoegen", (nameof(Components.Create.OnTagCreated), callback));
    }
}

