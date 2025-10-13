using MediatR;
using Microservice_Net9_.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

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

            });

            return group;
        }
    }
}
