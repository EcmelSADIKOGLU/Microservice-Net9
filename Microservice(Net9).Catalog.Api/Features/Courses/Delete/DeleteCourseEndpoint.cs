namespace Microservice_Net9_.Catalog.Api.Features.Courses.Delete
{
    public static class DeleteCourseEndpoint
    {
        public static IEndpointRouteBuilder DeleteCourseGroupItem(this IEndpointRouteBuilder group)
        {
            group.MapDelete("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteCourse");

            return group;
        }
    }
}
