using EShop.Domain.Entities.Cart;
using EShop.Domain.Interfaces.Repositories.Base;

namespace EShop.Domain.Interfaces.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetByUserId(int userId);
}
