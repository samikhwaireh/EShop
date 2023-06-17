using EShop.Domain.Entities.Order;
using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Repositories.Base;

namespace EShop.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
