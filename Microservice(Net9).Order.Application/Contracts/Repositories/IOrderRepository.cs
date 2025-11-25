using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<_Order, Guid>
    {
        public Task<List<_Order>> GetOrdersByUserId(Guid buyerId);
    }
}
