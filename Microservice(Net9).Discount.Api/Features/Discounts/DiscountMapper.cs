using Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount;

namespace Microservice_Net9_.Discount.Api.Features.Discounts
{
    public class DiscountMapper : Profile
    {
        public DiscountMapper()
        {
            CreateMap<CreateDiscountCommand, Discount>();
        }
    }
}
