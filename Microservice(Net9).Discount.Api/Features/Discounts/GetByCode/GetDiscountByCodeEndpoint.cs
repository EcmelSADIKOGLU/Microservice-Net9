using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Discount.Api.Features.Discounts.GetByCode
{
    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder GetDiscountByCodeEndpointGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/{code:length(10)}",
                async (IMediator mediator, string code) =>
                (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces<DiscountDto>(StatusCodes.Status200OK)
                .WithName("GetDiscountByCode");

            return group;
        }
    }
}
