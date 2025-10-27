using Microservice_Net9_.Shared;

namespace Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem
{
    public record AddBasketItemCommand(
        Guid CourseId,
        string CourseName,
        decimal CoursePrice,
        string? ImageUrl,
        Guid UserId,
        string EducatorFullName
        ):IRequestByServiceResult;
}
