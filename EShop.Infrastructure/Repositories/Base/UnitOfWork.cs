using EShop.Domain.Interfaces.Repositories;
using EShop.Domain.Interfaces.Repositories.Base;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EShop.Infrastructure.Repositories.Base;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    public ICartRepository Carts { get; }
    public ICategoryRepository Categories { get; }
    public IOrderRepository Orders { get; }
    public IProductRepository Products { get; }
    public IWishlistRepository Wishlists { get; }

    public UnitOfWork(ApplicationDbContext context, 
        ICartRepository cartRepository, 
        ICategoryRepository categoryRepository, 
        IOrderRepository orderRepository, 
        IProductRepository productRepository, 
        IWishlistRepository wishlistRepository)
    {
        Context = context;
        Carts = cartRepository;
        Categories = categoryRepository;
        Orders = orderRepository;
        Products = productRepository;
        Wishlists = wishlistRepository;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private readonly ApplicationDbContext Context;
    private IDbContextTransaction transaction;
    private bool disposed;


    public void Commit()
    {
        transaction?.Commit();
    }

    public void CreateTransaction()
    {
        transaction = Context.Database.BeginTransaction();
    }


    public void Rollback()
    {
        transaction.Rollback();
        transaction.Dispose();
    }

    public async Task SaveChanges()
    {
        await Context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool dispose)
    {
        if (!this.disposed)
        {
            if (dispose)
            {
                Context.Dispose();
            }
            this.disposed = true;
        }
    }
}
