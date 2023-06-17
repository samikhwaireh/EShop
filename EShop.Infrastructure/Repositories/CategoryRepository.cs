using EShop.Domain.Entities.ProductCatalog;
using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Repositories.Base;

namespace EShop.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository 
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
