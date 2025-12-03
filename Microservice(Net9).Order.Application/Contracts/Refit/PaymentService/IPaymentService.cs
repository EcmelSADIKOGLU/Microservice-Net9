using Refit;


namespace Microservice_Net9_.Order.Application.Contracts.Refit.PaymentService
{
    public interface IPaymentService 
    {
        [Post("/api/v1/payments")]
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest request);


    }
}
