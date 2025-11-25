using Microservice_Net9_.Shared;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.Create
{
    public record CreatePaymentCommand(
        string OrderCode, 
        string CardNumber, 
        string CardHolderName,
        DateTime ExpirationDate,
        string Cvv,
        decimal Amount
        ) : IRequestByServiceResult;
}
