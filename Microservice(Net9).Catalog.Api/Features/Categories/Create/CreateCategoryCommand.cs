namespace Microservice_Net9_.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryResponse>;  //primary constructor

}
