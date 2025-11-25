using MediatR;
using Microservice_Net9_.Payment.Api.Repositories;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Services;
using System.Net;
using _Payment = Microservice_Net9_.Payment.Api.Repositories.Payment;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandHandler(
        AppDbContext appDbContext,
        IIdentityService identityService
        ) : IRequestHandler<CreatePaymentCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {

            var payment = new _Payment(identityService.GetUserId, request.OrderCode, request.Amount);
            var result = await ExternalPaymentProcessAsync(request.CardNumber, request.CardHolderName, request.ExpirationDate, request.Cvv, request.Amount);

            if (!result.isSuccess)
            {
                payment.SetPaymentStatus(PaymentStatus.Failed);
                return ServiceResult.Error("Payment failed", result.errorMessage!, HttpStatusCode.BadRequest);
            }

            payment.SetPaymentStatus(PaymentStatus.Success);

            await appDbContext.Payments.AddAsync(payment, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }

        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, DateTime expirationDate, string cvv, decimal amount)
        {
            await Task.Delay(1000);
            return (true, null);
        }
    }
}
