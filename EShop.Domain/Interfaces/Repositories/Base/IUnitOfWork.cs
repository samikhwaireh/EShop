namespace EShop.Domain.Interfaces.Repositories.Base;

public interface IUnitOfWork
{
    ICartRepository Carts { get; }
    ICategoryRepository Categories { get; }
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    IWishlistRepository Wishlists { get; }

    void CreateTransaction();
    void Commit();
    void Rollback();
    Task SaveChanges();
}
