
using Microservice_Net9_.Discount.Api.Repositories;

namespace Microservice_Net9_.Discount.Api.Features.Discounts.GetByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<DiscountDto>>
    {
        public async Task<ServiceResult<DiscountDto>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {

            var discount = await context.Discounts.FirstOrDefaultAsync(x => x.Code == request.Code, cancellationToken);
            
            if(discount == null)
            {
                return ServiceResult<DiscountDto>.Error("Discount does not exist", $"Discount with status code {request.Code} could not found.", HttpStatusCode.NotFound);
            }

            if (discount.ExpireTime < DateTime.UtcNow)
            {
                return ServiceResult<DiscountDto>.Error("Discount Expired", $"Discount with code {request.Code} has expired.", HttpStatusCode.BadRequest);

            }

            var discountDto = mapper.Map<DiscountDto>(discount);
            return ServiceResult<DiscountDto>.SuccessAsOk(discountDto);

        }
    }
}
