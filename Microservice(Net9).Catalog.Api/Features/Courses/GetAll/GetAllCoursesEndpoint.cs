namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetAll
{
    public static class GetAllCoursesEndpoint 
    {
        public static RouteGroupBuilder GetAllCoursesGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                async (IMediator mediator) =>
                (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces<GetAllCoursesResponse>(StatusCodes.Status200OK)
                .WithName("GetAllCourses");

            return group;
        }
    }
}
