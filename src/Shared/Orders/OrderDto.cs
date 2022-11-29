using System;
using FluentValidation;

namespace BogusStore.Shared.Orders;

public abstract class OrderDto
{
    public class Create
    {
        public IEnumerable<OrderItemDto.Create> Items { get; set; } = default!;
        // Other properties such as :
        // DeliveryDate
        // ShippingAddress (when different from Customer Address),...

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                RuleFor(x => x.Items).NotEmpty();
                RuleForEach(x => x.Items).SetValidator(new OrderItemDto.Create.Validator());
            }
        }
    }
}

