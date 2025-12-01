using Microservice_Net9_.Shared;

namespace Microservice_Net9_.Order.Application.UseCases.Orders.GetById
{
    public record GetOrdersByBuyerIdQuery(Guid BuyerId) : IRequestByServiceResult<GetOrdersByBuyerIdResponse>;
}
