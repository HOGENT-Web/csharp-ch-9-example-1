using Microsoft.AspNetCore.Mvc;
using BogusStore.Shared.Products;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using BogusStore.Shared.Authentication;

namespace BogusStore.Server.Controllers.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [SwaggerOperation("Returns a list of products available in the bogus catalog.")]
    [HttpGet, AllowAnonymous]
    public async Task<ProductResult.Index> GetIndex([FromQuery] ProductRequest.Index request)
    {
        return await productService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns a specific product available in the bogus catalog.")]
    [HttpGet("{productId}"), AllowAnonymous]
    public async Task<ProductDto.Detail> GetDetail(int productId)
    {
        return await productService.GetDetailAsync(productId);
    }

    [SwaggerOperation("Creates a new product in the catalog.")]
    [HttpPost, Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Create(ProductDto.Mutate model)
    {
        var productId = await productService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), productId);
    }

    [SwaggerOperation("Edites an existing product in the catalog.")]
    [HttpPut("{productId}")]
    public async Task<IActionResult> Edit(int productId, ProductDto.Mutate model)
    {
        await productService.EditAsync(productId, model);
        return NoContent();
    }

    [SwaggerOperation("Deletes an existing product in the catalog.")]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await productService.DeleteAsync(productId);
        return NoContent();
    }

    [SwaggerOperation("Adds an existing tag to an existing product.")]
    [HttpPost("{productId}/tags/{tagId}")]
    public async Task<IActionResult> AddTag(int productId, int tagId)
    {
        await productService.AddTagAsync(productId, tagId);
        return Ok();
    }
}
