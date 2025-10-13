using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItem(this RouteGroupBuilder group)
        {
            // http://localhost:5000/api/categories/
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);

                return new ObjectResult(result)
                {
                    StatusCode = result.StatusCode.GetHashCode(),
                };

            });

            return group;
        }
    }
}
