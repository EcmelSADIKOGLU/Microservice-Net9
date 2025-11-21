using MediatR;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Extensions;
using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.File.Api.Features.File.Upload
{
    public static class UploadFileEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file, IMediator mediator) =>
            {
                var result = await mediator.Send(new UploadFileCommand(file));
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("UploadFile")
                .Produces<ServiceResult<UploadFileCommandResponse>>(StatusCodes.Status201Created)
                .DisableAntiforgery();

            return group;
        }
    }
}
