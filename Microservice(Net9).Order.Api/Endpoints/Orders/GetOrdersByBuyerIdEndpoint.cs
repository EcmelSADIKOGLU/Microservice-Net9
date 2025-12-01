using MediatR;
using Microservice_Net9_.Order.Application.UseCases.Orders.GetById;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Extensions;

namespace Microservice_Net9_.Order.Api.Endpoints.Orders
{
    public static class GetOrdersByBuyerIdEndpoint
    {
        public static RouteGroupBuilder GetOrdersByBuyerIdGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{buyerId:guid}", async (Guid buyerId, IMediator mediator) =>
            {

                var result = await mediator.Send(new GetOrdersByBuyerIdQuery(buyerId));
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("GetOrdersByBuyerId")
                .Produces<ServiceResult>(StatusCodes.Status200OK);

            return group;
        }
    }
}
