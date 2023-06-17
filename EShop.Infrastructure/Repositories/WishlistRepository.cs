using EShop.Domain.Entities.ProductCatalog.Wishlists;
using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Repositories.Base;

namespace EShop.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
