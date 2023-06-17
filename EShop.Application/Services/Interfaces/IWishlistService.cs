using EShop.Application.Models;

namespace EShop.Application.Services.Interfaces;

public interface IWishlistService
{
    Task<WishlistDto> GetWishlistByUserId(int userId);
    Task AddItem(int userId, int productId);
    Task RemoveItem(int wishlistId, int productId);
}
