using Microservice_Net9_.Order.Application.Dtos;
using Microservice_Net9_.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Order.Application.Features.Orders.GetById
{
    public record GetOrdersByBuyerIdQuery(Guid BuyerId) : IRequestByServiceResult<GetOrdersByBuyerIdResponse>;
}
