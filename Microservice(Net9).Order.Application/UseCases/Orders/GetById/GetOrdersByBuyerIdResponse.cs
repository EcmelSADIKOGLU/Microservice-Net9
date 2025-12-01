using Microservice_Net9_.Order.Application.Dtos;

namespace Microservice_Net9_.Order.Application.UseCases.Orders.GetById
{
    public record GetOrdersByBuyerIdResponse(List<OrderDto> Orders);
}
