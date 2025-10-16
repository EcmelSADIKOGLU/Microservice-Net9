using AutoMapper;
using MediatR;
using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;
using Microservice_Net9_.Catalog.Api.Repositories;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Extensions;
using Microsoft.EntityFrameworkCore;



namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetAll
{

    public record GetAllCategoryQuery : IRequestByServiceResult<GetAllCategoryResponse>;
    public record GetAllCategoryResponse(List<CategoryDto> Categories);
    public class GetAllCategoryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<GetAllCategoryResponse>>
    {
        public async Task<ServiceResult<GetAllCategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var data = await context.Categories.ToListAsync(cancellationToken);
            var response = new GetAllCategoryResponse(mapper.Map<List<CategoryDto>>(data)); 
            return ServiceResult<GetAllCategoryResponse>.SuccessAsOk(response);

        }
    }

    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                async (IMediator mediator) =>
                (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());

            return group;
        }
    }


    


}
