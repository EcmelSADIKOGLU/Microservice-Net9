using Microservice_Net9_.Catalog.Api.Features.Categories.Dtos;
using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<GetAllCategoryResponse>>
    {
        public async Task<ServiceResult<GetAllCategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var data = await context.Categories.ToListAsync(cancellationToken);
            var response = new GetAllCategoryResponse(mapper.Map<List<CategoryDto>>(data));
            return ServiceResult<GetAllCategoryResponse>.SuccessAsOk(response);

        }
    }
}
