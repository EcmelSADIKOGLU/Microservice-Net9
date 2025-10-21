namespace Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount
{
    public record CreateDiscountCommand (
        Guid UserId,
        float Rate,
        string Code,
        DateTime? ExpireTime
        ) : IRequestByServiceResult<Guid>;
}
