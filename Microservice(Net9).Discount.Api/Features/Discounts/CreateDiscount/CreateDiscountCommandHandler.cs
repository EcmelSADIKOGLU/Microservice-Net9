
using Microservice_Net9_.Discount.Api.Repositories;

namespace Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateDiscountCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = mapper.Map<Discount>(request);
            discount.CreateTime = DateTime.UtcNow;
            await context.Discounts.AddAsync(discount, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            
            return ServiceResult<Guid>.SuccessAsCreated(discount.Id, $"/api/discounts/{discount.Id}");
        }
    }
}
