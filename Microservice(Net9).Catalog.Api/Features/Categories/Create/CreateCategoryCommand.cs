using MediatR;
using Microservice_Net9_.Shared;

namespace Microservice_Net9_.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;  //primary constructor




    //public class x 
    //{
    //    public string Name { get; init; } = default!; //bir defa setlenince tekrar çalışamaz

    //    public x(string name)
    //    {
    //        Name = name;
    //    }
    //}
}
