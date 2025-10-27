using FluentValidation;

namespace Microservice_Net9_.Basket.Api.Features.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(x=>x.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
