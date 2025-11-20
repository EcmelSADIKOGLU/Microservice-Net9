using Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem;
using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItem(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("ApplyDiscountCoupon")
                .Produces(StatusCodes.Status204NoContent)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();

            return group;
        }
    }
}
