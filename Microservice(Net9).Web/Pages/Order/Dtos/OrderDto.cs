namespace Microservice_Net9_.Web.Pages.Order.Dtos;

public record OrderDto(
    DateTime OrderDate,
    decimal Total,
    List<OrderItemDto> OrderItems
);
