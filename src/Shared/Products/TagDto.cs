using FluentValidation;

namespace BogusStore.Shared.Products;

public abstract class TagDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
    }

    public class Mutate
    {
        public string? Name { get; set; }
        public string? Color { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Color).NotEmpty().MaximumLength(100);
            }
        }
    }

}

