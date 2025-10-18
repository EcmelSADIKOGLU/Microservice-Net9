using FluentValidation;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} can not be empty.")
                .Length(4, 100).WithMessage("{PropertyName} must be between 4 and 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} can not be empty.")
                .Length(4, 1000).WithMessage("{PropertyName} must be between 4 and 1000 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.CategoryId)
               .NotEmpty().WithMessage("{PropertyName} can not be empty.");

            RuleFor(x => x.ImageUrl)
               .MaximumLength(500).WithMessage("{PropertyName} must be at most 500 characters.");


            
        }
    }
}
