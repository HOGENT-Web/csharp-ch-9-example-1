﻿using System;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BogusStore.Persistence.Configurations.Customers;

internal class CustomerConfiguration : EntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);
        builder.OwnsOne(x => x.Email, email =>
        {
            email.HasIndex(x => x.Value).IsUnique();
            email.Property(x => x.Value).HasColumnName(nameof(Customer.Email));
        });

        builder.OwnsOne(x => x.Address, address =>
        {
            // Without this mapping EF Core does not save the properties since they're getters only.
            // This can be omitted by making them private set, but then you're lying to the domain model.
            address.Property(x => x.Addressline1).HasColumnName(nameof(Address.Addressline1));
            address.Property(x => x.Addressline2).HasColumnName(nameof(Address.Addressline2));
            address.Property(x => x.PostalCode).HasColumnName(nameof(Address.PostalCode));
            address.Property(x => x.City).HasColumnName(nameof(Address.City));
            address.Property(x => x.Country).HasColumnName(nameof(Address.Country));
        });
    }
}