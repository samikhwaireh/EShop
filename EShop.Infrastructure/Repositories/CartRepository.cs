using EShop.Domain.Entities.Cart;
using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Cart> GetByUserId(int userId)
    {
        return await Entity.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
