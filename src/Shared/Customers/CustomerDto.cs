using FluentValidation;

namespace BogusStore.Shared.Customers;

public abstract class CustomerDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public AddressDto? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Mutate
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; } = default!;
        public AddressDto Address { get; set; } = new();

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Firstname).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Lastname).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Address).NotEmpty().SetValidator(new AddressDto.Validator());
            }
        }
    }
}

