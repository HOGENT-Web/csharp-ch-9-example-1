using System;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BogusStore.Persistence.Configurations.Customers;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsOne(x => x.Email).Property(x => x.Value);
        builder.OwnsOne(x => x.Address, address =>
        {
            // Without this mapping EF Core does not save the properties since they're getters only.
            // This can be omitted by making them private set, but then you're lying to the domain model.
            address.Property(x => x.Addressline1);
            address.Property(x => x.Addressline2);
            address.Property(x => x.City);
            address.Property(x => x.Country);
            address.Property(x => x.PostalCode);
        });
    }
}