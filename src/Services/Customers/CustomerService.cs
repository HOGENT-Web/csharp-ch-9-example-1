using BogusStore.Domain.Customers;
using BogusStore.Domain.Products;
using BogusStore.Persistence;
using BogusStore.Shared.Customers;
using BogusStore.Shared.Products;
using Microsoft.EntityFrameworkCore;

namespace BogusStore.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly BogusDbContext dbContext;

    public CustomerService(BogusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CustomerResult.Index> GetIndexAsync(CustomerRequest.Index request)
    {
        var query = dbContext.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Firstname.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase)
                                  || x.Email.Value.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase)
                                  || x.Lastname.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new CustomerDto.Index
           {
               Id = x.Id,
               Firstname = x.Firstname,
               Lastname = x.Lastname,
               Email = x.Email.Value,
           }).ToListAsync();

        var result = new CustomerResult.Index
        {
            Customers = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    public async Task<CustomerDto.Detail> GetDetailAsync(int customerId)
    {
        CustomerDto.Detail? customer = await dbContext.Customers.Select(x => new CustomerDto.Detail
        {
            Id = x.Id,
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            Email = x.Email.Value,
            Address = new AddressDto
            {
                Addressline1 = x.Address.Addressline1,
                Addressline2 = x.Address.Addressline2,
                PostalCode = x.Address.PostalCode,
                City = x.Address.City,
                Country = x.Address.Country,
            },
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
        }).SingleOrDefaultAsync(x => x.Id == customerId);

        if (customer is null)
            throw new EntityNotFoundException(nameof(Customer), customerId);

        return customer;
    }

    public async Task<int> CreateAsync(CustomerDto.Mutate model)
    {
        if (await dbContext.Customers.AnyAsync(x => x.Email.Value == model.Email))
            throw new EntityAlreadyExistsException(nameof(Customer), nameof(Customer.Email), model.Email);

        EmailAddress email = new EmailAddress(model.Email);
        Address address = new(model.Address.Addressline1!, model.Address.Addressline2, model.Address.PostalCode!, model.Address.City!, model.Address.Country!);
        Customer customer = new(model.Firstname!, model.Lastname!, address, email);

        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();

        return customer.Id;
    }

    public async Task EditAsync(int customerId, CustomerDto.Mutate model)
    {
        Customer? customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId);

        if (customer is null)
            throw new EntityNotFoundException(nameof(Customer), customerId);

        // If this becomes to much, refactor to an Edit method in the Customer domain class.
        customer.Firstname = model.Firstname!;
        customer.Lastname = model.Lastname!;
        customer.Email = new EmailAddress(model.Email);
        customer.Address = new Address(model.Address.Addressline1!, model.Address.Addressline2, model.Address.PostalCode!, model.Address.City!, model.Address.Country!); ;

        await dbContext.SaveChangesAsync();
    }
}

