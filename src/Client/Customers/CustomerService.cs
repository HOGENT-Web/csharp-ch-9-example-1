using System;
using System.Net.Http.Json;
using BogusStore.Client.Extensions;
using BogusStore.Shared.Customers;
using BogusStore.Shared.Products;

namespace BogusStore.Client.Customers;

public class CustomerService : ICustomerService
{
    private readonly HttpClient client;
    private const string endpoint = "api/customer";
    public CustomerService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CustomerResult.Index> GetIndexAsync(CustomerRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<CustomerResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }

    public async Task<CustomerDto.Detail> GetDetailAsync(int customerId)
    {
        var response = await client.GetFromJsonAsync<CustomerDto.Detail>($"{endpoint}/{customerId}");
        return response;
    }

    public async Task<int> CreateAsync(CustomerDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task EditAsync(int customerId, CustomerDto.Mutate model)
    {
        var response = await client.PutAsJsonAsync($"{endpoint}/{customerId}", model);
    }
}

