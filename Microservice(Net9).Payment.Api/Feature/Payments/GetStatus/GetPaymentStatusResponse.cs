namespace Microservice_Net9_.Payment.Api.Feature.Payments.GetStatus;

public record GetPaymentStatusRepsonse(bool isPaid, Guid? PaymentId);
