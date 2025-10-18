using Microservice_Net9_.Catalog.Api.Features.Courses.Dtos;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetById
{
    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByIdGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult())
                .Produces<CourseDto>(StatusCodes.Status200OK)
                .WithName("GetCourseById");

            return group;
        }
    }
}
