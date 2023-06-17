using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers;

[ApiController]
[Route("api/carts")]
public class CartController : ControllerBase
{
    private readonly ICartService cartService;

    public CartController(ICartService cartService)
    {
        this.cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }

    [HttpPost("{userId}/items/{productId}")]
    public async Task<IActionResult> AddItem(int userId, int productId)
    {
        await cartService.AddItem(userId, productId);
        return Ok();
    }

    [HttpPost("{userId}/clear")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        await cartService.ClearCart(userId);
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCartByUserId(int userId)
    {
        var cart = await cartService.GetCartByUserId(userId);
        return Ok(cart);
    }

    [HttpDelete("{cartId}/items/{cartItemId}")]
    public async Task<IActionResult> RemoveItem(int cartId, int cartItemId)
    {
        await cartService.RemoveItem(cartId, cartItemId);
        return Ok();
    }
}