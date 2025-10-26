namespace Microservice_Net9_.Discount.Api.Features.Discounts.GetByCode
{
    public record GetDiscountByCodeQuery(string Code) : IRequestByServiceResult<DiscountDto>;

}
