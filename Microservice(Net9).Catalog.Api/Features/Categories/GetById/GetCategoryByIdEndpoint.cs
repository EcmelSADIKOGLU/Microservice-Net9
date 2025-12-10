using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;
using Microservice_Net9_.Catalog.Api.Features.Categories.GetById;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetCategoryById
{

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryByIdGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces<CategoryDto>(StatusCodes.Status200OK)
                .WithName("GetCategoryById")
                .RequireAuthorization(policyNames: "ClientCredential"); ;

            return group;
        }
    }
}
