using Microservice_Net9_.Web.Pages.Order.Dtos;

namespace Microservice_Net9_.Web.Services.ResponseDtos.Order;

public record GetOrdersByBuyerIdResponse(List<OrderDto> Orders);
