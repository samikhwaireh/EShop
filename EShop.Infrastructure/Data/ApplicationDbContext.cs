using EShop.Domain.Entities.Cart;
using EShop.Domain.Entities.Order;
using EShop.Domain.Entities.ProductCatalog.Wishlists;
using EShop.Domain.Entities.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EShop.Infrastructure.Identity.Models;

namespace EShop.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }

    public DbSet<ProductWishlist> ProductWishlists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        SetTableNamesAsSingle(builder);

        base.OnModelCreating(builder);

        builder.Entity<Order>(ConfigureOrder);
        builder.Entity<Product>(ConfigureProduct);

        builder.Entity<ProductWishlist>(ConfigureProductWishlist);
    }

    private static void SetTableNamesAsSingle(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
        }
    }

    private void ConfigureOrder(EntityTypeBuilder<Order> builder)
    {

    }

    private void ConfigureProduct(EntityTypeBuilder<Product> builder)
    {
        
    }

    private void ConfigureProductWishlist(EntityTypeBuilder<ProductWishlist> builder)
    {
        builder.HasKey(pw => new { pw.ProductId, pw.WishlistId });
    }

}
