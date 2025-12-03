namespace Microservice_Net9_.Payment.Api.Feature.Payments.Create;

public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);

    