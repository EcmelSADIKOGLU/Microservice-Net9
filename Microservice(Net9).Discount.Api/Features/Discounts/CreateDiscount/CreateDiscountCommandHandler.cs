
using Microservice_Net9_.Discount.Api.Repositories;

namespace Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateDiscountCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {

            var any = await context.Discounts.AnyAsync(x => x.UserId == request.UserId && x.Code == request.Code, cancellationToken);

            if (any)
            {
                return ServiceResult<Guid>.Error("Discount Already Exists", $"A discount with code '{request.Code}' already exists for the user.", HttpStatusCode.BadRequest);
            }

            var discount = mapper.Map<Discount>(request);

            discount.CreateTime = DateTime.UtcNow;
            discount.Id = NewId.NextSequentialGuid();

            await context.Discounts.AddAsync(discount, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            
            return ServiceResult<Guid>.SuccessAsCreated(discount.Id, $"/api/v1/discounts/{discount.Id}");
        }
    }
}
