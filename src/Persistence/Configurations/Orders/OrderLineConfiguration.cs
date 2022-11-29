using System;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Orders;
using BogusStore.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BogusStore.Persistence.Configurations.Orders;

internal class OrderLineConfiguration : EntityConfiguration<OrderLine>
{
    public override void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        base.Configure(builder);

        // Was not mapped due to {get;}
        builder.Property(x => x.Quantity);
        builder.Property(x => x.Description);

        // Value Object Mapping and rename of column
        builder.OwnsOne(x => x.Price)
               .Property(x => x.Value)// Was not mapped due to {get;}
               .HasColumnName(nameof(OrderLine.Price));

        // 1 to Many relationship with a cascade restrict behavior
        builder.HasOne(x => x.Product)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
    }
}