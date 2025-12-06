namespace Microservice_Net9_.Order.Application.Contracts.Refit.PaymentService
{
    public record GetPaymentStatusRepsonse(bool isPaid, Guid? PaymentId);
}
