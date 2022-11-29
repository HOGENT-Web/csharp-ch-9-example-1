using FluentValidation;

namespace BogusStore.Shared.Products;

public abstract class ProductDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsInStock { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }

    public class Mutate
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageContentType { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(1_000);
                RuleFor(x => x.Price).InclusiveBetween(0,1_000);
                RuleFor(x => x.ImageContentType).NotEmpty().WithName("Image");
            }
        }
    }
}

