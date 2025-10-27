using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Basket.Api.Features.Basket.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItem(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{courseId:guid}",
                async (IMediator mediator, Guid courseId) =>
                (await mediator.Send(new DeleteBasketItemCommand(courseId))).ToGenericResult())
                .AddEndpointFilter<ValidationFilter<DeleteBasketItemCommand>>()
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteBasketItem");

            return group;
        }
    }
}
