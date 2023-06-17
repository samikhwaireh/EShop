namespace EShop.Domain.Entities.ProductCatalog.Wishlists;

public class ProductWishlist
{
    public int WishlistId { get; set; }
    public Wishlist Wishlist { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

}
