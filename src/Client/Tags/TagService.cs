using BogusStore.Client.Extensions;
using BogusStore.Shared.Products;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BogusStore.Client.Tags;

public class TagService : ITagService
{
    private readonly HttpClient client;
    private const string endpoint = "api/tag";
    public TagService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<int> CreateAsync(TagDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public Task EditAsync(int tagId, TagDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public async Task<TagResult.Index> GetIndexAsync(TagRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<TagResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }
}
