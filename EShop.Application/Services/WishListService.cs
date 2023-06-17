using AutoMapper;
using EShop.Application.Mapper;
using EShop.Application.Models;
using EShop.Application.Services.Interfaces;
using EShop.Domain.Entities.ProductCatalog.Wishlists;
using EShop.Domain.Interfaces.Repositories.Base;

namespace EShop.Application.Services;

public class WishListService : IWishlistService
{
    private readonly IUnitOfWork unitOfWork;

    public WishListService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task AddItem(int userId, int productId)
    {
        unitOfWork.CreateTransaction();

        var wishlist = await GetExistingOrCreateNewWishlist(userId);
        wishlist.AddItem(productId);
        unitOfWork.Wishlists.Update(wishlist);
        unitOfWork.Commit();
        await unitOfWork.SaveChanges();
    }

    public async Task<WishlistResponse> GetWishlistByUserId(int userId)
    {
        var wishlist = await GetExistingOrCreateNewWishlist(userId);
        var wishlistModel = AppMapper.Mapper.Map<WishlistResponse>(wishlist);

        foreach (var item in wishlist.ProductWishlists)
        {
            var product = await unitOfWork.Products.GetBy(p => p.Id == item.ProductId);
            var productModel = AppMapper.Mapper.Map<ProductResponse>(product);
            wishlistModel.Items.Add(productModel);
        }

        return wishlistModel;
    }

    public async Task RemoveItem(int wishlistId, int productId)
    {
        var wishlist = await unitOfWork.Wishlists.GetBy(p => p.Id == wishlistId);
        wishlist.RemoveItem(productId);
        unitOfWork.Wishlists.Update(wishlist);
        await unitOfWork.SaveChanges();
    }

    private async Task<Wishlist> GetExistingOrCreateNewWishlist(int userId)
    {
        var wishlist = await unitOfWork.Wishlists.GetBy(w => w.UserId == userId);
        if (wishlist != null)
            return wishlist;

        var newWishlist = new Wishlist
        {
            UserId = userId
        };

        await unitOfWork.Wishlists.AddAsync(newWishlist);
        return newWishlist;
    }
}
