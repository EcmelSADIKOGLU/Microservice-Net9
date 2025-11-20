using Microservice_Net9_.Basket.Api.Dto;

namespace Microservice_Net9_.Basket.Api.Features.Basket.GetBasket
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/user",
                async (IMediator mediator) =>
                (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces<BasketDto>(StatusCodes.Status200OK)
                .WithName("GetBasket");

            return group;
        }
    }
}
