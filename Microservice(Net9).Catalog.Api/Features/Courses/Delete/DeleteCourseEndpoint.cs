namespace Microservice_Net9_.Catalog.Api.Features.Courses.Delete
{
    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItem(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteCourse");

            return group;
        }
    }
}
