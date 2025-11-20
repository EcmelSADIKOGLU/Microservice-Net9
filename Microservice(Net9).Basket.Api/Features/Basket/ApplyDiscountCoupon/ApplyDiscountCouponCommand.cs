namespace Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand (string CouponCode, float DiscountRate): IRequestByServiceResult;
}
