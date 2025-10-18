using Microservice_Net9_.Catalog.Api.Repositories;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
             var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

            if (existCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Error("Category Name already exist", $"The category name {request.Name} is already exist", System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                var category = new Category { 
                    Name = request.Name,
                    Id = NewId.NextSequentialGuid(),
                };

                await context.Categories.AddAsync(category, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), $"/api/categories/{category.Id}");

            }
        }
    }
}
