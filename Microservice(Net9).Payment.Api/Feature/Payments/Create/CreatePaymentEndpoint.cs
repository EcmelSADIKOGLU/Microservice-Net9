using MediatR;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Filters;
using Microservice_Net9_.Shared.Extensions;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.Create
{
    public static class CreatePaymentEndpoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreatePaymentCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("CreatePayment")
                .Produces<ServiceResult<Guid>>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreatePaymentCommand>>();

            return group;
        }
    }
}
