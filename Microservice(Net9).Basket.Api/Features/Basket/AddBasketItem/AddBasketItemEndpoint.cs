using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("AddBasketItem")
                .Produces(StatusCodes.Status204NoContent)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();

            return group;
        }
    }
}
