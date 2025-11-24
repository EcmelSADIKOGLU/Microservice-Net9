using MediatR;
using Microservice_Net9_.Order.Application.Features.Orders.Create;
using Microservice_Net9_.Shared.Filters;
using Microservice_Net9_.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microservice_Net9_.Shared;

namespace Microservice_Net9_.Order.Api.Endpoints.Orders
{
    public static class CreateOrderEndpoint
    {
        public static RouteGroupBuilder CreateOrderGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] CreateOrderCommand command, [FromServices]IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("CreateOrder")
                .Produces<ServiceResult>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

            return group;
        }
    }
}
