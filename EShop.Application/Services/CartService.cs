using AutoMapper;
using EShop.Application.Mapper;
using EShop.Application.Models;
using EShop.Application.Services.Interfaces;
using EShop.Domain.Entities.Cart;
using EShop.Domain.Interfaces.Repositories;
using EShop.Domain.Interfaces.Repositories.Base;

namespace EShop.Application.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task AddItem(int userId, int productId)
    {
        var cart = await GetExistingOrCreateNewCart(userId);

        var product = await unitOfWork.Products.GetByIdAsync(productId);

        if (product == null)
        {
            throw new ApplicationException("Failed to get product");
        }

        cart.AddItem(productId, unitPrice: product.UnitPrice);
        unitOfWork.Carts.Update(cart);
        await unitOfWork.SaveChanges();
    }

    public async Task ClearCart(int userId)
    {
        var cart = await unitOfWork.Carts.GetByUserId(userId);
        if (cart == null)
            throw new ApplicationException("No Cart Found");

        cart.ClearItems();

        unitOfWork.Carts.Update(cart);
        await unitOfWork.SaveChanges();
    }

    public async Task<CartDto> GetCartByUserId(int userId)
    {
        var cart = await GetExistingOrCreateNewCart(userId);
        var cartResponse = AppMapper.Mapper.Map<CartDto>(cart);

        if (cart.Items.Any(c => c.Product == null))
        {
            cartResponse.Items.Clear();
            foreach (var item in cart.Items)
            {
                var cartItemModel = AppMapper.Mapper.Map<CartItemDto>(item);
                var product = await unitOfWork.Products.GetByIdAsync(item.ProductId);
                var productModel = AppMapper.Mapper.Map<ProductDto>(product);
                cartItemModel.Product = productModel;
                cartResponse.Items.Add(cartItemModel);
            }
        }

        return cartResponse;
    }

    public async Task RemoveItem(int cartId, int cartItemId)
    {
        Cart cart = await unitOfWork.Carts.GetByIdAsync(cartId);
        cart.RemoveItem(cartItemId);
        unitOfWork.Carts.Update(cart);
        await unitOfWork.SaveChanges();
    }

    private async Task<Cart> GetExistingOrCreateNewCart(int userId)
    {
        var cart = await unitOfWork.Carts.GetByUserId(userId);
        if (cart != null)
            return cart;

        var newCart = new Cart
        {
            UserId = userId,
        };

        await unitOfWork.Carts.AddAsync(newCart);
        await unitOfWork.SaveChanges();
        return newCart;
    }
}
