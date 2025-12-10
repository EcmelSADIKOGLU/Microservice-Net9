using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;
using Microservice_Net9_.Catalog.Api.Repositories;



namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetAll
{

    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                async (IMediator mediator) =>
                (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces<GetAllCategoryResponse>(StatusCodes.Status200OK)
                .WithName("GetAllCategory")
                .RequireAuthorization(policyNames: "ClientCredential");

            return group;
        }
    }

}
