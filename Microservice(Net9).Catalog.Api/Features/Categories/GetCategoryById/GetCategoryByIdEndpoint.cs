using AutoMapper;
using MediatR;
using Microservice_Net9_.Catalog.Api.Features.Categories.Create;
using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;
using Microservice_Net9_.Catalog.Api.Features.Categories.GetAll;
using Microservice_Net9_.Catalog.Api.Repositories;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Extensions;
using Microservice_Net9_.Shared.Filters;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetCategoryById
{

    public record GetCategoryByIdQuery(Guid id) : IRequestByServiceResult<CategoryDto>;
    
    public class GetCategoryByIdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.id, cancellationToken);

            if (category is null)
            {
                return ServiceResult<CategoryDto>.ErrorAsNotFound();
            }

            var response = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.SuccessAsOk(response);
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryByIdGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult());

            return group;
        }
    }
}
