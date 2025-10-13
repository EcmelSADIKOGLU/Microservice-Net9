using MediatR;
using Microservice_Net9_.Catalog.Api.Features.Categories.Create;
using Microservice_Net9_.Catalog.Api.Repositories;
using Microservice_Net9_.Shared;
using MongoDB.Driver.Linq;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryEndpoint
    {

    }

    public record GetAllCategoryQuery : IRequest<ServiceResult<GetAllCategoryResponse>>;

    public record GetAllCategoryResponseItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public class GetAllCategoryResponse
    {
        public List<GetAllCategoryResponseItem> Categories { get; set; }
    }

    public class GetAllCategoryHandler(AppDbContext context) : IRequestHandler<GetAllCategoryQuery, ServiceResult<GetAllCategoryResponse>>
    {
        public async Task<ServiceResult<GetAllCategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var data = await context.Categories.ToListAsync();
            var response = new GetAllCategoryResponse()
            {
                Categories = (from item in data
                              select new GetAllCategoryResponseItem
                              {
                                  Id = item.Id,
                                  Name = item.Name,
                              }).ToList()
            };

            return ServiceResult<GetAllCategoryResponse>.SuccessAsOk(response);

        }
    }
}
