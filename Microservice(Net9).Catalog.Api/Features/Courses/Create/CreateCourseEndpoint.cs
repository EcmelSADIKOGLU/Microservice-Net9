using Microservice_Net9_.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromForm]CreateCourseCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("CreateCourse")
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>()
                .RequireAuthorization("instructor")
                .DisableAntiforgery(); //multipart form data requests are not supported by antiforgery

            return group;
        }
    }
}
