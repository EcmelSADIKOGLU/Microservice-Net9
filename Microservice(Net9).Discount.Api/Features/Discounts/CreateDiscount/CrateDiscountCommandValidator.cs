using FluentValidation;
using Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount;

namespace Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CrateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CrateDiscountCommandValidator()
        {
            RuleFor(x => x.Rate)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .Length(3, 10).WithMessage("{PropertyName} must be between 3 and 10 characters.");

            RuleFor(x => x.ExpireTime)
                .GreaterThan(DateTime.Now).WithMessage("{PropertyName} must be a future date")
                .When(x => x.ExpireTime.HasValue);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must be a valid GUID");

        }
    }
}
