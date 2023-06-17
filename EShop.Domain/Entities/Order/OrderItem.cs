using EShop.Domain.Entities.Common;
using EShop.Domain.Entities.ProductCatalog;

namespace EShop.Domain.Entities.Order;
public class OrderItem : Entity
{
    public int Quantity { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
