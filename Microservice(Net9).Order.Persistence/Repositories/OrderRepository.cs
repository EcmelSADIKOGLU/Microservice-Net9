using Microservice_Net9_.Order.Application.Contracts.Repositories;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<_Order, Guid>(context), IOrderRepository
    {

    }
}
