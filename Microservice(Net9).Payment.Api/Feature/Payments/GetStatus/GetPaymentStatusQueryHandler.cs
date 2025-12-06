using MediatR;
using Microservice_Net9_.Payment.Api.Repositories;
using Microservice_Net9_.Shared;
using Microsoft.EntityFrameworkCore;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.GetStatus
{
    public class GetPaymentStatusQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetPaymentStatusQuery, ServiceResult<GetPaymentStatusRepsonse>>
    {
        public async Task<ServiceResult<GetPaymentStatusRepsonse>> Handle(GetPaymentStatusQuery request, CancellationToken cancellationToken)
        {
            var payment = await appDbContext.Payments.FirstOrDefaultAsync(p => p.OrderCode == request.OrderCode);
            if (payment == null) 
            {
                return ServiceResult<GetPaymentStatusRepsonse>.SuccessAsOk(new GetPaymentStatusRepsonse(false, null));
            }

            return ServiceResult<GetPaymentStatusRepsonse>.SuccessAsOk(new GetPaymentStatusRepsonse(true, payment.Id));

        }
    }
}
