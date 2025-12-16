namespace Microservice_Net9_.Web.Pages.Basket.Dtos;

public record AddBasketItemRequest(
    Guid CourseId,
    string CourseName,
    decimal CoursePrice,
    string? ImageUrl,
    string EducatorFullName
    );
