using FluentValidation;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{Property Name} can not be empty")
                .Length(4, 25).WithMessage("{Property Name} must be between 4 and 25 characters.");
        }
    }

}
