using System.Reflection;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Products;
using BogusStore.Persistence.Triggers;
using Microsoft.EntityFrameworkCore;

namespace BogusStore.Persistence;

public class BogusDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Customer> Customers => Set<Customer>();

    public BogusDbContext(DbContextOptions<BogusDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // All decimals should have 2 digits after the comma
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        // Max Length of a NVARCHAR that can be indexed
        configurationBuilder.Properties<string>().HaveMaxLength(4_000);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

