namespace BogusStore.Shared.Products;

public interface ITagService
{
    Task<int> CreateAsync(TagDto.Mutate model);
    Task EditAsync(int tagId, TagDto.Mutate model);
    Task<TagResult.Index> GetIndexAsync(TagRequest.Index request);
}