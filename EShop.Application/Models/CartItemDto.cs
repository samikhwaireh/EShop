namespace EShop.Application.Models;

public class CartItemDto
{
    public int Quantity { get; set; }
    public string Color { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int ProductId { get; set; }
    public ProductDto Product { get; set; }
}
