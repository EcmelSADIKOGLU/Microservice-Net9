namespace Microservice_Net9_.Order.Application.Contracts.Refit.PaymentService
{
    public record CreatePaymentRequest(
        string OrderCode, 
        string CardNumber, 
        string CardHolderName,
        DateTime ExpirationDate,
        string Cvv,
        decimal Amount
        );
}
