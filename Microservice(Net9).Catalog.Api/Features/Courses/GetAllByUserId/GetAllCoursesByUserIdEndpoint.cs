using Microservice_Net9_.Catalog.Api.Features.Courses.GetAll;

namespace Microservice_Net9_.Catalog.Api.Features.Courses.GetAllByUserId
{
    public static class GetAllCoursesByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllCoursesByUserIdGroupItem(this RouteGroupBuilder group)
        {

            group.MapGet("/GetAllCoursesByUserId/{id:guid}",
            async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCoursesByUserIdQuery(id));
                return result.ToGenericResult();
            })
            .Produces<GetAllCoursesResponse>(StatusCodes.Status200OK)
            .WithName("GetAllCoursesByUserId");


            return group;
        }
    }
}
