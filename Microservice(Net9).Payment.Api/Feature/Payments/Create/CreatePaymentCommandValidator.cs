using FluentValidation;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandValidator: AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.OrderCode)
           .NotEmpty().WithMessage("OrderCode is required.")
           .Length(10).WithMessage("OrderCode must be exactly 10 characters.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .CreditCard().WithMessage("Invalid card number format.")
                .Length(13, 19).WithMessage("Card number must be between 13 and 19 digits.")
                .Matches("^[0-9]+$").WithMessage("Card number must contain only digits.");

            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("Card holder name is required.")
                .MaximumLength(100).WithMessage("Card holder name cannot exceed 100 characters.");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("Expiration date is required.")
                .Must(date => date > DateTime.UtcNow.Date).WithMessage("Expiration date must be in the future.");

            RuleFor(x => x.Cvv)
                .NotEmpty().WithMessage("CVV is required.")
                .Matches("^[0-9]{3,4}$").WithMessage("CVV must be 3 or 4 digits.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
