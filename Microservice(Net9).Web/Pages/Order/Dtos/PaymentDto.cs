namespace Microservice_Net9_.Web.Pages.Order.Dtos;

public record PaymentDto(
    string CardNumber,
    string CardHolderName,
    DateTime ExpirationDate,
    string Cvv,
    decimal Amount = 1
    );
