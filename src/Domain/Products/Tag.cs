namespace BogusStore.Domain.Products;

/// <summary>
/// A tag or label to mark products.
/// </summary>
public class Tag : Entity
{
    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
    }

    private string color = default!;
    public string Color
    {
        get => color;
        set => color = Guard.Against.NullOrWhiteSpace(value, nameof(Color));
    }

    private readonly List<Product> products = new();
    public IReadOnlyCollection<Product> Products => products.AsReadOnly();

    /// <summary>
    /// Database Constructor
    /// </summary>
    private Tag() { }

    public Tag(string name, string color)
    {
        Name = name;
        Color = color;
    }
}

