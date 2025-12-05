using Asp.Versioning.Builder;
using Microservice_Net9_.Payment.Api.Feature.Payments.Create;
using Microservice_Net9_.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;
using Microservice_Net9_.Payment.Api.Feature.Payments.GetStatus;

namespace Microservice_Net9_.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("Payments")
                .WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItem()
                .GetAllPaymentsByUserIdGroupItem()
                .GetPaymentStatusGroupItem();

        }
    }
}
