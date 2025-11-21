namespace Microservice_Net9_.Basket.Api.Features.Basket.ClearDiscountCoupon
{
    public static class ClearDiscountCouponCommandEndpoint
    {
        public static RouteGroupBuilder ClearDiscountCouponGroupItem(this RouteGroupBuilder group)
        {
            group.MapDelete("/clear-discount-coupon", async (IMediator mediator) =>    
            {

                var result = await mediator.Send(new ClearDiscountCouponCommand());
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("ClearDiscountCoupon")
                .Produces(StatusCodes.Status204NoContent);

            return group;
        }
    }
}
