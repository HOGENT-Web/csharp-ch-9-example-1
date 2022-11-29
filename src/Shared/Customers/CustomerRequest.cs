using System;
namespace BogusStore.Shared.Customers;

public abstract class CustomerRequest
{
    public class Index
    {
        public string? Searchterm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}


