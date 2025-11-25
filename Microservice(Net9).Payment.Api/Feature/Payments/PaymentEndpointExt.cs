using Asp.Versioning.Builder;
using Microservice_Net9_.Payment.Api.Feature.Payments.Create;

namespace Microservice_Net9_.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("Payments")
                .WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItem();

        }
    }
}
