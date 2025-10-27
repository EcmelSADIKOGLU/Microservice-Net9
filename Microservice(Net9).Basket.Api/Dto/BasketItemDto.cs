namespace Microservice_Net9_.Basket.Api.Dto
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
