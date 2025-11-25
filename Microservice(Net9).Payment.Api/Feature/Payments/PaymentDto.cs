using Microservice_Net9_.Payment.Api.Repositories;

namespace Microservice_Net9_.Payment.Api.Feature.Payments
{
    public record PaymentDto(
        Guid Id,
        string OrderCode,
        string Amount,
        DateTime CreatedTime,
        PaymentStatus Status);

}
