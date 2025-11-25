using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Order.Application.Dtos
{
    public record OrderDto(
        DateTime OrderDate,
        decimal Total,
        List<OrderItemDto> OrderItems
    );
}
