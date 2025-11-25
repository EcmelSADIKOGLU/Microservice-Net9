using MediatR;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Extensions;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentsByUserIdGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllPaymentsByUserIdQuery());
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("GetAllPaymentsByUserId")
                .Produces<ServiceResult>(StatusCodes.Status200OK);

            return group;
        }

    }
}
