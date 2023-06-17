using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers;

[ApiController]
[Route("api/wishlists")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        this.wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
    }

    [HttpPost("{userId}/items/{productId}")]
    public async Task<IActionResult> AddItem(int userId, int productId)
    {
        await wishlistService.AddItem(userId, productId);
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetWishlistByUserId(int userId)
    {
        var wishlist = await wishlistService.GetWishlistByUserId(userId);
        return Ok(wishlist);
    }

    [HttpDelete("{wishlistId}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(int wishlistId, int productId)
    {
        await wishlistService.RemoveItem(wishlistId, productId);
        return Ok();
    }
}
