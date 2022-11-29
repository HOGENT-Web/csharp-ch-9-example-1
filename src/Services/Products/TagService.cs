using System;
using BogusStore.Domain.Products;
using BogusStore.Persistence;
using BogusStore.Shared.Products;
using Microsoft.EntityFrameworkCore;

namespace BogusStore.Services.Products;

public class TagService : ITagService
{
    private readonly BogusDbContext dbContext;

    public TagService(BogusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<TagResult.Index> GetIndexAsync(TagRequest.Index request)
    {
        var query = dbContext.Tags.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new TagDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               Color = x.Color
           }).ToListAsync();

        var result = new TagResult.Index
        {
            Tags = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    public async Task<int> CreateAsync(TagDto.Mutate model)
    {
        if (await dbContext.Tags.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Tag), nameof(Tag.Name), model.Name);

        Tag tag = new(model.Name!, model.Color!);

        dbContext.Tags.Add(tag);
        await dbContext.SaveChangesAsync();

        return tag.Id;
    }

    public async Task EditAsync(int tagId, TagDto.Mutate model)
    {
        Tag? tag = await dbContext.Tags.SingleOrDefaultAsync(x => x.Id == tagId);

        if (tag is null)
            throw new EntityNotFoundException(nameof(Tag), tagId);

        tag.Name = model.Name!;
        tag.Color = model.Color!;

        await dbContext.SaveChangesAsync();
    }
}

