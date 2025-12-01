using MediatR;
using Microservice_Net9_.Payment.Api.Repositories;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdQueryHandler(
        AppDbContext appDbContext,
        IIdentityService identityService
        ): IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<GetAllPaymentsByUserIdResponse>>
    {
        public async Task<ServiceResult<GetAllPaymentsByUserIdResponse>> Handle(GetAllPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var paymentDtos = await appDbContext.Payments
                .Where(p => p.BuyerId == identityService.UserId)
                .Select(p => new PaymentDto(
                    Id: p.Id,
                    OrderCode: p.OrderCode,
                    Amount: p.Amount.ToString("C"),
                    CreatedTime: p.CreatedTime,
                    Status: p.Status
                )).ToListAsync();

            return ServiceResult<GetAllPaymentsByUserIdResponse>.SuccessAsOk(new GetAllPaymentsByUserIdResponse(paymentDtos));
        }
    }
}
