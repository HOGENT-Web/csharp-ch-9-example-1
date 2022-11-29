using System;
using BogusStore.Shared.Products;

namespace BogusStore.Shared.Customers;

public abstract class CustomerResult
{
    public class Index
    {
        public IEnumerable<CustomerDto.Index>? Customers { get; set; }
        public int TotalAmount { get; set; }
    }
}

