using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class CrateDiscountEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return result.ToGenericResult();

            })
                .MapToApiVersion(1, 0)
                .WithName("CreateDiscount")
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

            return group;
        }
    }
}
