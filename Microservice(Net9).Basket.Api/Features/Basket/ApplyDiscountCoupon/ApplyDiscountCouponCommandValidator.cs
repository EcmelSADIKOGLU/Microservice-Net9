using FluentValidation;

namespace Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandValidator:AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.CouponCode)
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(x => x.DiscountRate)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThanOrEqualTo(1).WithMessage("{PropertyName} must be less than or equal to 1.");
        }
    }
}
