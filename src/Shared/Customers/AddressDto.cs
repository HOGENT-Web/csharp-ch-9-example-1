using FluentValidation;

namespace BogusStore.Shared.Customers;

public class AddressDto
{
    public string? Addressline1 { get; set; }
    public string? Addressline2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

    public class Validator : AbstractValidator<AddressDto>
    {
        public Validator()
        {
            RuleFor(x => x.Addressline1).NotEmpty().MaximumLength(250);
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(20);
            RuleFor(x => x.City).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
        }
    }
}

