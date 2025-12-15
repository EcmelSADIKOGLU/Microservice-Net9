namespace Microservice_Net9_.Web.Pages.Basket.Dtos
{
    public record BasketItemDto(
        Guid CourseId,
        string CourseName,
        decimal CoursePrice,
        string? ImageUrl,
        Guid UserId, //EducatorUserId
        string EducatorFullName,
        decimal? PriceByApplyDiscount
    );

}
