using MediatR;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_Net9_.File.Api.Features.File.Delete
{
    public static class DeleteFileEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItem(this RouteGroupBuilder group)
        {
            group.MapDelete("/", async ([FromBody] DeleteFileCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("DeleteFile")
                .Produces<ServiceResult>(StatusCodes.Status204NoContent);

            return group;
        }
    }
}
