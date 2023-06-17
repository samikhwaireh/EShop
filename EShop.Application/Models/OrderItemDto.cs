using EShop.Domain.Entities.ProductCatalog;

namespace EShop.Application.Models;

public class OrderItemDto
{
    public int Quantity { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public Product Product { get; set; }
}