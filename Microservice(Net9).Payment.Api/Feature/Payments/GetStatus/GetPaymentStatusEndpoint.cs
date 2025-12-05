using MediatR;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_Net9_.Payment.Api.Feature.Payments.GetStatus
{
    public static class GetPaymentStatusEndpoint
    {
        public static RouteGroupBuilder GetPaymentStatusGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/status/{orderCode}", async (string orderCode, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetPaymentStatusQuery(orderCode));
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("GetPaymentStatus")
                .Produces<ServiceResult<GetPaymentStatusRepsonse>>(StatusCodes.Status200OK)
                .RequireAuthorization("ClientCredential");

            return group;
        }
    }
}
