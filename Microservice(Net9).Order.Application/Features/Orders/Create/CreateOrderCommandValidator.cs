using FluentValidation;
using Microservice_Net9_.Order.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.DiscountRate)
                .GreaterThan(0).When(x => x.DiscountRate.HasValue)
                .WithMessage("{PropertyName} must be a positive number or zero");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("{PropertyName} is required")
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("{PropertyName} must contain at least one order item");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemDtoValidator());

            RuleFor(x => x.Payment)
                .NotNull().WithMessage("{PropertyName} is required")
                .SetValidator(new PaymentDtoValidator());
        }
    }

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Line)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Matches(@"^\d{5}$").WithMessage("{PropertyName} must be 5 digits");
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
        }
    }

    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.Cvv)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(3, 4).WithMessage("{PropertyName} must be 3 or 4 digits");

            RuleFor(x => x.ExpirationDate)
                .Must(date => date > DateTime.UtcNow)
                .WithMessage("{PropertyName} must be a future date");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
        }
    }
}

