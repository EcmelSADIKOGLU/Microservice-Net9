using Microservice_Net9_.Order.Application.Dtos;
using Microservice_Net9_.Shared;

namespace Microservice_Net9_.Order.Application.UseCases.Orders.Create
{
    public record CreateOrderCommand
        (float? DiscountRate, 
        AddressDto Address, 
        PaymentDto Payment,
        List<OrderItemDto> OrderItems
        ) : IRequestByServiceResult;
}
