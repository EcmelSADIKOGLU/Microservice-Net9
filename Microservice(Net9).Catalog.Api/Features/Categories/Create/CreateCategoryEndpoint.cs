using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .WithName("CreateCategory")
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }
    }
}
