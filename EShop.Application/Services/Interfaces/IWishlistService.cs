using EShop.Application.Models;

namespace EShop.Application.Services.Interfaces;

public interface IWishlistService
{
    Task<WishlistResponse> GetWishlistByUserId(int userId);
    Task AddItem(int userId, int productId);
    Task RemoveItem(int wishlistId, int productId);
}
