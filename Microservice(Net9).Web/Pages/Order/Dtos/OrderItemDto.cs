namespace Microservice_Net9_.Web.Pages.Order.Dtos;

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice
    );
