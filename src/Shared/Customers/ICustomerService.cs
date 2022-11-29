using System;
using BogusStore.Shared.Products;

namespace BogusStore.Shared.Customers;

public interface ICustomerService
{
    Task<CustomerDto.Detail> GetDetailAsync(int customerId);
    Task<CustomerResult.Index> GetIndexAsync(CustomerRequest.Index request);
    Task<int> CreateAsync(CustomerDto.Mutate model);
    Task EditAsync(int customerId, CustomerDto.Mutate model);
}

