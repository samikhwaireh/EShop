namespace EShop.Application.Models;

public class CartResponse
{
    public int UserId { get; set; }
    public List<CartItemResponse> Items { get; set; }
}
