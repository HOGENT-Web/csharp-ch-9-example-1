using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BogusStore.Shared.Customers;
using BogusStore.Services.Products;
using BogusStore.Shared.Products;
using BogusStore.Shared.Orders;

namespace BogusStore.Server.Controllers.Products;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService customerService;
    private readonly IOrderService orderService;

    public CustomerController(ICustomerService customerService, IOrderService orderService)
    {
        this.customerService = customerService;
        this.orderService = orderService;
    }

    [SwaggerOperation("Returns a list of all the customers.")]
    [HttpGet]
    public async Task<CustomerResult.Index> GetIndex([FromQuery] CustomerRequest.Index request)
    {
        return await customerService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns a specific customer.")]
    [HttpGet("{customerId}")]
    public async Task<CustomerDto.Detail> GetDetail(int customerId)
    {
        return await customerService.GetDetailAsync(customerId);
    }

    [SwaggerOperation("Creates a new customer.")]
    [HttpPost]
    public async Task<IActionResult> Create(CustomerDto.Mutate model)
    {
        var customerId = await customerService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), customerId);
    }

    [SwaggerOperation("Edites an existing customer.")]
    [HttpPut("{customerId}")]
    public async Task<IActionResult> Edit(int customerId, CustomerDto.Mutate model)
    {
        await customerService.EditAsync(customerId, model);
        return NoContent();
    }

    [SwaggerOperation("Places an order for an existing customer.")]
    [HttpPost("{customerId}/place-order")]
    public async Task<IActionResult> PlaceOrder(int customerId, OrderDto.Create model)
    {
        int orderId = await orderService.CreateAsync(customerId, model);
        return CreatedAtAction(nameof(PlaceOrder), new { id = orderId });
    }
}
