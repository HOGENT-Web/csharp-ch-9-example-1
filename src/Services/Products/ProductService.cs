using System.Linq;
using BogusStore.Domain.Files;
using BogusStore.Domain.Products;
using BogusStore.Persistence;
using BogusStore.Services.Files;
using BogusStore.Shared.Products;
using Microsoft.EntityFrameworkCore;

namespace BogusStore.Services.Products;

public class ProductService : IProductService
{
    private readonly BogusDbContext dbContext;
    private readonly IStorageService storageService;

    public ProductService(BogusDbContext dbContext, IStorageService storageService)
    {
        this.dbContext = dbContext;
        this.storageService = storageService;
    }

    public async Task<ProductResult.Index> GetIndexAsync(ProductRequest.Index request)
    {
        var query = dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        if (request.MinPrice is not null)
        {
            query = query.Where(x => x.Price.Value >= request.MinPrice);
        }

        if (request.MaxPrice is not null)
        {
            query = query.Where(x => x.Price.Value <= request.MaxPrice);
        }

        if (request.TagId is not null)
        {
            query = query.Where(x => x.Tags.Select(x => x.Id).Contains(request.TagId.Value));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new ProductDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               Description = x.Description,
               Price = x.Price.Value,
               ImageUrl = x.ImageUrl,
           }).ToListAsync();

        var result = new ProductResult.Index
        {
            Products = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    public async Task<ProductDto.Detail> GetDetailAsync(int productId)
    {
        ProductDto.Detail? product = await dbContext.Products.Select(x => new ProductDto.Detail
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price.Value,
            Description = x.Description,
            Tags = x.Tags.Select(x => x.Name),
            ImageUrl = x.ImageUrl,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product),productId);

        return product;
    }

    public async Task<ProductResult.Create> CreateAsync(ProductDto.Mutate model)
    {
        if (await dbContext.Products.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Product), nameof(Product.Name), model.Name);

        Image image = new Image(storageService.BasePath, model.ImageContentType!);
        Money price = new(model.Price);
        Product product = new(model.Name!, model.Description!, price, image.FileUri.ToString());

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        Uri uploadSas = storageService.GenerateImageUploadSas(image);

        ProductResult.Create result = new()
        {
            ProductId = product.Id,
            UploadUri = uploadSas.ToString()
        };

        return result;
    }


    public async Task EditAsync(int productId, ProductDto.Mutate model)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product), productId);

        Money price = new(model.Price);
        product.Name = model.Name!;
        product.Description = model.Description!;
        product.Price = price;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int productId)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product),productId);

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync();
    }

    public async Task AddTagAsync(int productId, int tagId)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product), productId);

        Tag? tag = await dbContext.Tags.SingleOrDefaultAsync(x => x.Id == tagId);

        if (tag is null)
            throw new EntityNotFoundException(nameof(Tag), tagId);

        product.Tag(tag);

        await dbContext.SaveChangesAsync();
    }
}

