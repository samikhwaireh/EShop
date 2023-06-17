using EShop.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> GetCartByUserId(int userId);
        Task AddItem(int userId, int productId);
        Task RemoveItem(int cartId, int cartItemId);
        Task ClearCart(int userId);
    }
}
