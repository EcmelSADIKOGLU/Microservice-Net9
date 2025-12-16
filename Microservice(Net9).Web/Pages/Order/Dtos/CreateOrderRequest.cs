namespace Microservice_Net9_.Web.Pages.Order.Dtos;

public record CreateOrderRequest
    (float? DiscountRate, 
    AddressDto Address, 
    PaymentDto Payment,
    List<OrderItemDto> OrderItems
    );
