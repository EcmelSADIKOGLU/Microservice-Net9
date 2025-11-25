using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<_Order, Guid>(context), IOrderRepository
    {
        public Task<List<_Order>> GetOrdersByUserId(Guid buyerId)
        {
            return context.Orders
                .Include(o => o.OrderItems)
                //.Include(o => o.Address)
                .Where(o => o.BuyerId == buyerId)
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();
        }
    }
}
