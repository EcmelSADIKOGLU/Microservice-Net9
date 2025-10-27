using FluentValidation;

namespace Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(x => x.CoursePrice)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
            
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EducatorFullName)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

        }
    }
}
