using Microservice_Net9_.Shared;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.GetStatus
{
    public record GetPaymentStatusQuery(string OrderCode): IRequestByServiceResult<GetPaymentStatusRepsonse>;
}
