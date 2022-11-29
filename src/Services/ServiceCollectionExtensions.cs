using BogusStore.Services.Customers;
using BogusStore.Services.Files;
using BogusStore.Services.Orders;
using BogusStore.Services.Products;
using BogusStore.Shared.Customers;
using BogusStore.Shared.Orders;
using BogusStore.Shared.Products;
using Microsoft.Extensions.DependencyInjection;

namespace BogusStore.Services;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all services to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBogusServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IStorageService, BlobStorageService>();

        // Add more services here...

        return services;
    }
}

