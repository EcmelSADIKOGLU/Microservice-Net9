using System.ComponentModel.DataAnnotations;

namespace Microservice_Net9_.Web.Pages.Order.ViewModels;

public record PaymentViewModel
{
    [Display(Name = "Card Number")] public string CardNumber { get; set; } = null!;

    [Display(Name = "Cardholder Name")] public string CardHolderName { get; set; } = null!;

    [Display(Name = "Expiry Date")] public string ExpiryDate { get; set; } = null!;

    [Display(Name = "CVV")] public string Cvv { get; set; } = null!;

    [Display(Name = "Payment Amount")] public decimal Amount { get; set; }

    public static PaymentViewModel Empty => new();

    public static PaymentViewModel Example => new()
    {
        Amount = 250,
        CardHolderName = "John Doe",
        CardNumber = "4111411141114111",
        Cvv = "123",
        ExpiryDate = "12/27"
    };

}
