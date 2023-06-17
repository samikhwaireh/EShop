using EShop.Domain.Entities.Common;
using EShop.Domain.Entities.ProductCatalog;

namespace EShop.Domain.Entities.Cart;

public class CartItem : Entity
{
    public int Quantity { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
