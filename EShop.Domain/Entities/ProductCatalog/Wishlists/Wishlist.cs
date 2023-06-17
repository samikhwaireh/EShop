using EShop.Domain.Entities.Common;

namespace EShop.Domain.Entities.ProductCatalog.Wishlists;

public class Wishlist : Entity
{
    public int UserId { get; set; }
    public List<ProductWishlist> ProductWishlists { get; set; } = new List<ProductWishlist>();

    public void AddItem(int productId)
    {
        var existingItem = ProductWishlists.FirstOrDefault(x => x.ProductId == productId);
        if (existingItem != null)
            return;

        ProductWishlists.Add(new ProductWishlist
        {
            ProductId = productId,
            WishlistId = this.Id
        });
    }

    public void RemoveItem(int productId)
    {
        var removedItem = ProductWishlists.FirstOrDefault(x => x.ProductId == productId);
        if (removedItem != null)
        {
            ProductWishlists.Remove(removedItem);
        }
    }
}
