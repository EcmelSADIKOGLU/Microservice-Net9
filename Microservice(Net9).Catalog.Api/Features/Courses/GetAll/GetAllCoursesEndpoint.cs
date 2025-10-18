namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetAll
{
    public static class GetAllCoursesEndpoint 
    {
        public static RouteGroupBuilder GetAllCoursesGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                async (IMediator mediator) =>
                (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
                .Produces<GetAllCoursesResponse>(StatusCodes.Status200OK)
                .WithName("GetAllCourses");

            return group;
        }
    }
}
