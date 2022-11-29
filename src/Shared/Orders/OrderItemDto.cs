using System;
using FluentValidation;

namespace BogusStore.Shared.Orders;

public class OrderItemDto
{
    public class Create
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                RuleFor(x => x.ProductId).NotEmpty();
                RuleFor(x => x.Quantity).NotEmpty();
            }
        }
    }
}

