using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItem(this RouteGroupBuilder group)
        {
            group.MapPut("/update", async (UpdateCourseCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>()
                .WithName("UpdateCourse")
                .Produces<ServiceResult>(StatusCodes.Status204NoContent)
                .Produces<ServiceResult>(StatusCodes.Status404NotFound);
            return group;
        }
    }
}
